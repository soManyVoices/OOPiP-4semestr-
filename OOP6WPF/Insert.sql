USE Personnel;
-- ���������� ������� � ������� "Job"
INSERT INTO Job (Name) VALUES ('��������');
INSERT INTO Job (Name) VALUES ('�������');
INSERT INTO Job (Name) VALUES ('�������');
INSERT INTO Job (Name) VALUES ('�����������');

-- ���������� ������� � ������� "Status"
INSERT INTO Status (Name) VALUES ('�����/�������');
INSERT INTO Status (Name) VALUES ('������/����������');
INSERT INTO Status (Name) VALUES ('� �������');
INSERT INTO Status (Name) VALUES ('������������/�����');

-- ���������� ������� � ������� "Person"
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('���� ������', '�������', 30, 1, 1);
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('����� �������', '�������', 25, 2, 2);
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('��������� �������', '�������', 22, 3, 3);
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('����� ���������', '�������', 35, 4, 4);
