-- Create Facility Table
CREATE TABLE FacilityTable (
    ID INT PRIMARY KEY,
    Name NVARCHAR(100),
    Address NVARCHAR(200),
    City NVARCHAR(100),
    PostCode NVARCHAR(20),
    OpenTime TIME,
    CloseTime TIME
)
-- Create Membership Table
CREATE TABLE MembershipTable (
    ID INT PRIMARY KEY,
    Price DECIMAL(10, 2),
    ShowerAccess BIT,
    Tier NVARCHAR(100),
    PoolAccessStartTime TIME,
    PoolAccessEndTime TIME
)
-- Create Customer Table
CREATE TABLE CustomerTable (
    ID INT PRIMARY KEY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(255),
    Password NVARCHAR(255),
    Salt VARBINARY(MAX),
    BirthDate DATETIME,
    StartDate DATETIME,
    EndDate DATETIME,
    MembershipID INT NULL,
    AttendanceCount INT,
    PurchasesMade INT,
    FOREIGN KEY (MembershipID) REFERENCES MembershipTable(ID)
)
-- Add Customer-Membership FK
ALTER TABLE MembershipTable
ADD CustomerID INT, CONSTRAINT FK_Membership_Customer FOREIGN KEY (CustomerID) REFERENCES CustomerTable(ID)

-- Create Employee Table
CREATE TABLE EmployeeTable (
    ID INT PRIMARY KEY,
    FirstName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(255),
    Password NVARCHAR(255),
    Salt VARBINARY(MAX),
    BirthDate DATETIME,
    StartTime DATETIME,
    EndTime DATETIME,
    WorkDays NVARCHAR(100),
    Teaching BIT NULL,
    FacilityID INT NULL,
    FOREIGN KEY (FacilityID) REFERENCES FacilityTable(ID)
)

-- Create Pool Table
CREATE TABLE PoolTable (
    ID INT PRIMARY KEY,
    FacilityID INT NOT NULL,
    Type NVARCHAR(100),
    Booked BIT NOT NULL,
    FOREIGN KEY (FacilityID) REFERENCES FacilityTable(ID)
)

-- Create Lesson Table
CREATE TABLE LessonTable (
    ID INT PRIMARY KEY,
    Type NVARCHAR(100),
    PersonCount INT,
    StartTime DATETIME,
    EndTime DATETIME,
    TeacherID INT NOT NULL,
    PoolID INT NOT NULL,
    FOREIGN KEY (TeacherID) REFERENCES EmployeeTable(ID),
    FOREIGN KEY (PoolID) REFERENCES PoolTable(ID)
)

-- Create Locker Table
CREATE TABLE LockerTable (
    ID INT PRIMARY KEY,
    Rented BIT NOT NULL,
    FacilityID INT NOT NULL,
    OwnerID INT NULL,
    StartRent DATETIME,
    EndRent DATETIME,
    FOREIGN KEY (FacilityID) REFERENCES FacilityTable(ID),
    FOREIGN KEY (OwnerID) REFERENCES CustomerTable(ID)
)

-- Create Equipment Table
CREATE TABLE EquipmentTable (
    ID INT PRIMARY KEY,
    Rented BIT NOT NULL,
    Type NVARCHAR(100),
    Price DECIMAL(10, 2),
    FacilityID INT NOT NULL,
    OwnerID INT NULL,
    StartRent DATETIME,
    EndRent DATETIME,
    FOREIGN KEY (FacilityID) REFERENCES FacilityTable(ID),
    FOREIGN KEY (OwnerID) REFERENCES CustomerTable(ID)
)

-- Create Merchandise Table
CREATE TABLE MerchandiseTable (
    ID INT PRIMARY KEY,
    Price DECIMAL(10, 2),
    FacilityID INT NOT NULL,
    Type NVARCHAR(100),
    FOREIGN KEY (FacilityID) REFERENCES FacilityTable(ID)
)

-- Create Order Table
CREATE TABLE OrderTable (
    RowID INT PRIMARY KEY,
    OrderType NVARCHAR(100),
    CustomerID INT NOT NULL,
    OrderDate DATETIME,
    OrderPrice DECIMAL(10, 2),
    FacilityID INT NULL,
    FOREIGN KEY (CustomerID) REFERENCES CustomerTable(ID),
    FOREIGN KEY (FacilityID) REFERENCES FacilityTable(ID)
)

-- Create CustomerLesson Junction Table
CREATE TABLE CustomerLessonTable (
    ID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    LessonID INT NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES CustomerTable(ID),
    FOREIGN KEY (LessonID) REFERENCES LessonTable(ID)
)

-- Insert Facility Data
INSERT INTO FacilityTable (Name, Address, City, PostCode, OpenTime, CloseTime)
VALUES
('Sunnydale Gym', '123 Sunshine St', 'Sunnydale', 'SS12 3XY', '08:00:00', '20:00:00'),
('Mountain Health Center', '456 Mountain Rd', 'Hilltop', 'HT34 5AB', '07:00:00', '19:00:00'),
('Lakeside Spa', '789 Lakeside Blvd', 'Lakeside City', 'LC98 7GH', '09:00:00', '22:00:00'),
('Ocean Breeze Wellness', '321 Ocean Ave', 'Coastal Town', 'CT54 8JK', '10:00:00', '18:00:00'),
('Green Valley Clinic', '654 Green Dr', 'Valley View', 'VV01 2LM', '06:00:00', '17:00:00')

-- Insert Employee Data
INSERT INTO EmployeeTable (FirstName, LastName, BirthDate, StartTime, EndTime, WorkDays, Teaching, FacilityID, Email, Password, Salt)
VALUES
-- 
('John', 'Carter', '1980-05-15 00:00:00.0000000','2025-04-17 12:00:00.0000000', '2025-04-17 15:00:00.0000000', 'Tuesday,Wednesday,Thursday', NULL, 1, 'john.carter@email.com','Cy9sLib0GfJjAqGSqJIGcUWqFGxTPjrsDfdTVhNeaUY=', 0xDF2BD1143924A07D0008E86C0FB20A2B),
('Jane', 'Thompson', '1990-07-20 00:00:00.0000000', '00:00:00.0000000', '00:00:00.0000000', '', NULL, 2, 'jane.thompson@email.com', 'Lk0HXiGwmtgAbwn8bempx2NXarU07EPu+7VuQxSEbb4=', 0x8E1742DB3FA12609F2FD2792549462BA),
('Emily', 'Davis', '1985-03-25 00:00:00.0000000', '00:00:00.0000000', '00:00:00.0000000', '', NULL, 3, 'emily.davis@email.com', 'ZiCNVafqNMIZsyi424Pcn3BBAvHTRJIRjKh56hfppaE=', 0xF5D98C0CD69FA5DE36C60851328F4DD6),
('Michael', 'Wilson', '1992-10-05 00:00:00.0000000', '00:00:00.0000000', '00:00:00.0000000', '', NULL, 4, 'michael.wilson@email.com', 'hbDA7Dxj0H4RaCg4i5Vyz6Kz5foxOF5mA2yZA9ySiyo=', 0xCC171C1115801B046AC86674B21FF08A),
('Sarah', 'Miller', '1988-11-30 00:00:00.0000000', '00:00:00.0000000', '00:00:00.0000000', '', NULL, 5, 'sarah.miller@email.com', 'N1bKFY5GcYrVsZmZVrIMLptoQYedEqI9021sY3eQt6k=', 0x36B444E2BBABD60281AB68D6EF828F33);
-- All employee passwords = Password123

-- Insert Customer Data
INSERT INTO CustomerTable 
(FirstName, LastName, BirthDate, StartDate, EndDate, MembershipID, AttendanceCount, PurchasesMade, Email, Password, Salt)
VALUES
('Alice', 'Johnson', '1990-04-15 00:00:00.0000000', '0001-01-02 00:00:00.0000000', '0001-01-01 00:00:00.0000000', NULL, 0, 0, 'alice.johnson@email.com', 'ln5PGFS0+hwlxaVr3r1lx83MDHBEtC5Of3YaNN5jujQ=', 0xCB53392AC677A23E990C3C9FE487BCA6),
('Bob', 'Smith', '1985-07-22 00:00:00.0000000', '0001-01-02 00:00:00.0000000', '0001-01-01 00:00:00.0000000', NULL, 0, 0, 'bob.smith@email.com', 'YPYAa+xdDub3rpW2B00vWOHNoDIu1N9DXjvOG6T2pj4=', 0x4463D9ACEA9509E338139D4A295B2FD9),
('Carla', 'Nguyen', '1992-11-03 00:00:00.0000000', '0001-01-02 00:00:00.0000000', '0001-01-01 00:00:00.0000000', NULL, 0, 0, 'carla.nguyen@email.com', 'PAXKXDtRYGeka0AFHDEVQdgMVyI1ELCjtAdUVis4BhI=', 0x51536E66478DDA972245A004C805A777),
('Dan', 'Martin', '1988-02-09 00:00:00.0000000', '0001-01-02 00:00:00.0000000', '0001-01-01 00:00:00.0000000', NULL, 0, 0, 'dan.martin@email.com', 'PPO6qk3yitqiZJ8nrWXM4e/D+SREepw9bA7XKXBIugY=', 0x3B0757066FAA5CE7836072E7A6076BE0),
('Ella', 'Han', '1995-09-18 00:00:00.0000000', '0001-01-02 00:00:00.0000000', '0001-01-01 00:00:00.0000000', NULL, 0, 0, 'ella.han@email.com', 'M1ydOJ4C31S8NmyLa8UtH7YGObeR0kYms9+HBOwYy8k=', 0xDD8E22286C676C4601E698B2AC08C362);
-- All customer passwords = Password123

-- Insert Pool Data
INSERT INTO PoolTable (FacilityID, Type, Booked)
VALUES
(1,'Indoor', 0),
(2, 'Outdoor', 0),
(3, 'Indoor', 0),
(4, 'Indoor', 0),
(5, 'Outdoor', 0)

-- Insert Lesson Data
INSERT INTO LessonTable (Type, PersonCount, StartTime, EndTime, TeacherID, PoolID)
VALUES
('Swimming', 0, '2025-04-01 09:00:00', '2025-04-01 10:00:00', 1, 1),
('Water Aerobics', 0, '2025-05-15 10:30:00', '2025-05-15 11:30:00', 2, 2),
('Yoga', 0, '2025-04-10 12:00:00', '2025-04-10 13:00:00', 3, 3), 
('Advanced Swimming', 0, '2025-04-03 14:00:00', '2025-04-03 15:00:00', 4, 4),
('Aquatic Therapy', 0, '2025-04-07 16:00:00', '2025-04-07 17:00:00', 5, 5)

-- Insert Merchandise Data
INSERT INTO MerchandiseTable (Price, Type, FacilityID) VALUES
(4.99, 'Swim Goggles', 1),
(6.95, 'Swim Fins', 2),
(2.50, 'Swim Cap', 3),
(7.99, 'Training Snorkel', 4),
(12.00, 'Competition Swimsuit', 5),
(1.99, 'Nose Clip', 1),
(3.95, 'Pull Buoy', 2),
(2.00, 'Water Bottle', 3),
(9.99, 'Swim Backpack', 4),
(3.49, 'Swim Earplugs', 5)

-- Insert Locker Data
INSERT INTO LockerTable (Rented, FacilityID, OwnerID, StartRent, EndRent)
VALUES 
(1, 1, 2, '2025-04-01 08:00:00', '2025-04-01 09:00:00'),
(1, 2, 3, '2025-05-15 09:30:00', '2025-05-15 10:30:00'),
(0, 3, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00'),
(1, 4, 4, '2025-04-10 07:45:00', '2025-04-10 08:45:00'),
(0, 5, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00')

-- Insert Membership Data
INSERT INTO MembershipTable (Price, ShowerAccess, Tier, PoolAccessStartTime, PoolAccessEndTime)
VALUES
(29.99, 1, 'Bronze', '06:00:00', '22:00:00'),
(39.99, 1, 'Silver', '09:00:00', '17:00:00'),
(49.99, 1, 'Gold', '12:00:00', '15:00:00')

-- Insert Equipment Data
INSERT INTO EquipmentTable (Rented, Type, Price, FacilityID, OwnerID, StartRent, EndRent)
VALUES 
(1, 'Towel', 2.50, 1, 2, '2025-04-10 08:00:00', '2025-04-10 09:00:00'),
(1, 'Yoga Mat', 5.00, 1, 3, '2025-04-11 09:00:00', '2025-04-11 10:00:00'),
(0, 'Swim Goggles', 3.75, 2, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00'),
(0, 'Resistance Band', 4.25, 2, NULL, '0001-01-01 00:00:00', '0001-01-01 00:00:00')

-- Insert CustomerLesson Data
INSERT INTO CustomerLessonTable (CustomerID, LessonID)
VALUES 
(2, 1),
(3, 1),
(4, 2),
(5, 3),
(1, 2);
