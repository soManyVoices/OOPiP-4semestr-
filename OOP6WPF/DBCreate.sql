-- �������� ���� ������ "Personnel"
CREATE DATABASE Personnel;

-- ������������ �� ���� ������ "Personnel"
USE Personnel;

-- �������� ������� "Job"
CREATE TABLE Job (
  ID INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(50) NOT NULL
);

-- �������� ������� "Status"
CREATE TABLE Status (
  ID INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(50) NOT NULL
);

-- �������� ������� "Person"
CREATE TABLE Person (
  ID INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
  Gender VARCHAR(10) NOT NULL,
  Age INT NOT NULL,
  Job_ID INT,
  Status_ID INT,
  CONSTRAINT FK_Person_Job FOREIGN KEY (Job_ID) REFERENCES Job(ID) ON DELETE SET NULL,
  CONSTRAINT FK_Person_Status FOREIGN KEY (Status_ID) REFERENCES Status(ID) ON DELETE SET NULL
);

