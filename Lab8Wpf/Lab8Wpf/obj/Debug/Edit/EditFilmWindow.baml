   M S B A M L   `   `   `  ����  Lab8Wpf    Lab8Wpf.EditFilmWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  	Edit Film��5   	   $	��300��6   $	��400��6(   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Title:��6   $��0��6"   $��0��6/   $��5q�6?   �� 5   
   -   5         ��Name$ TitleTextBox��5      $��0��6(   $��1��65   $��5q�6E   �� 5   
   $��Duration (minutes):��6   $��1��6/   $��0��6<   $��5q�6L   �� 5   
   -   5        $ DurationTextBox��5      $��1��6+   $��1��68   $��5q�6H   �� 5   
   $��Release Date:��6   $��2��6)   $��0��66   $��5q�6F   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ ReleaseDateDatePicker��5      $��2��64   $��1��6A   $��5q�6Q   �� 5   
   $��Country:��6   $��3��6$   $��0��61   $��5q�6A   �� 5   
   -   5         ��Name$ CountryComboBox��5      $��3��6,   $��1��69   $��5q�6I   �� 5   
   $��Genre:��6   $��4��6"   $��0��6/   $��5q�6?   �� 5   
   -   5        $ GenreComboBox��5      $��4��6*   $��1��67   $��5q�6G   �� 5   
   -   5         ��Name$ 
SaveButton��5      $
��Save��6%   $��5��64   $��0��6A   $��5q�6Q   6\   �� 5   
   -   5        $ CancelButton��5      $��Cancel��6'   $��5��68   $��1��6E   $��5q�6U   6`   5      5      