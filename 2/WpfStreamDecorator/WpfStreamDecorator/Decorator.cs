using System;
using System.IO;

namespace StreamDecoratorLibrary
{

    public abstract class StreamDecorator : Stream
    {
        protected Stream decoratedStream;
        public virtual void FlushBuffer() { }

        public StreamDecorator(Stream decoratedStream)
        {
            this.decoratedStream = decoratedStream ?? throw new ArgumentNullException(nameof(decoratedStream));
        }

        public override bool CanRead => decoratedStream.CanRead;
        public override bool CanSeek => decoratedStream.CanSeek;
        public override bool CanWrite => decoratedStream.CanWrite;
        public override long Length => decoratedStream.Length;

        public override long Position
        {
            get => decoratedStream.Position;
            set => decoratedStream.Position = value;
        }

        public override void Flush() => decoratedStream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            return decoratedStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return decoratedStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            decoratedStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            decoratedStream.Write(buffer, offset, count);
        }
    }

    
    public class BufferedStreamDecorator : StreamDecorator
    {
        private byte[] buffer;
        private int bufferSize;
        private int bufferPosition;

        public BufferedStreamDecorator(Stream decoratedStream, int bufferSize) : base(decoratedStream)
        {
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize), "Размер буфера не может быть равен 0.");

            this.bufferSize = bufferSize;
            buffer = new byte[bufferSize];
            bufferPosition = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count > bufferSize)
                throw new ArgumentException("Превышен обьем буфера.");

            
            int bytesRead = 0;
            if (bufferPosition > 0)
            {
                bytesRead = Math.Min(count, bufferPosition);
                Array.Copy(this.buffer, 0, buffer, offset, bytesRead);
                bufferPosition -= bytesRead;
                offset += bytesRead;
                count -= bytesRead;
            }

            
            if (count > 0)
            {
                bytesRead += decoratedStream.Read(buffer, offset, count);
            }

            return bytesRead;
        }

        public override void FlushBuffer()
        {


            bufferPosition = decoratedStream.Read(buffer, 0, bufferSize);
        }
    }
}
