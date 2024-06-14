USE Personnel;
-- Добавление записей в таблицу "Job"
INSERT INTO Job (Name) VALUES ('Служащий');
INSERT INTO Job (Name) VALUES ('Рабочий');
INSERT INTO Job (Name) VALUES ('Студент');
INSERT INTO Job (Name) VALUES ('Безработный');

-- Добавление записей в таблицу "Status"
INSERT INTO Status (Name) VALUES ('Женат/Замужем');
INSERT INTO Status (Name) VALUES ('Холост/Незамужняя');
INSERT INTO Status (Name) VALUES ('В разводе');
INSERT INTO Status (Name) VALUES ('Вдовствующий/Вдова');

-- Добавление записей в таблицу "Person"
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('Иван Иванов', 'Мужской', 30, 1, 1);
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('Мария Петрова', 'Женский', 25, 2, 2);
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('Александр Смирнов', 'Мужской', 22, 3, 3);
INSERT INTO Person (Name, Gender, Age, Job_ID, Status_ID) VALUES ('Елена Кузнецова', 'Женский', 35, 4, 4);
