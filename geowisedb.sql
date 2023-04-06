-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 05, 2022 at 11:38 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `geowisedb`
--

-- --------------------------------------------------------

--
-- Table structure for table `admins`
--

CREATE TABLE `admins` (
  `ID` int(11) NOT NULL,
  `Email` varchar(100) COLLATE utf8mb4_turkish_ci NOT NULL,
  `Password` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_turkish_ci;

--
-- Dumping data for table `admins`
--

INSERT INTO `admins` (`ID`, `Email`, `Password`) VALUES
(1, 'admin', 1234);

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE `categories` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `categories`
--

INSERT INTO `categories` (`id`, `name`) VALUES
(1, 'Araç Plakaları'),
(2, 'Yemek'),
(3, 'Tarihi Eser');

-- --------------------------------------------------------

--
-- Table structure for table `cities`
--

CREATE TABLE `cities` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `cuontry_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `cities`
--

INSERT INTO `cities` (`id`, `name`, `cuontry_id`) VALUES
(1, 'Adana', 1),
(2, 'Adıyaman', 1),
(3, 'Afyonkarahisar', 1),
(4, 'Ağrı', 1),
(5, 'Amasya', 1),
(6, 'Ankara', 1),
(7, 'Antalya', 1),
(8, 'Artvin', 1),
(9, 'Aydın', 1),
(10, 'Balıkesir', 1),
(11, 'Bilecik', 1),
(12, 'Bingöl', 1),
(13, 'Bitlis', 1),
(14, 'Bolu', 1),
(15, 'Burdur', 1),
(16, 'Bursa', 1),
(17, 'Çanakkale', 1),
(18, 'Çankırı', 1),
(19, 'Çorum', 1),
(20, 'Denizli', 1),
(21, 'Diyarbakır', 1),
(22, 'Edirne', 1),
(23, 'Elazığ', 1),
(24, 'Erzincan', 1),
(25, 'Erzurum', 1),
(26, 'Eskişehir', 1),
(27, 'Gaziantep', 1),
(28, 'Giresun', 1),
(29, 'Gümüşhane', 1),
(30, 'Hakkari', 1),
(31, 'Hatay', 1),
(32, 'Isparta', 1),
(33, 'Mersin', 1),
(34, 'İstanbul', 1),
(35, 'İzmir', 1),
(36, 'Kars', 1),
(37, 'Kastamonu', 1),
(38, 'Kayseri', 1),
(39, 'Kırklareli', 1),
(40, 'Kırşehir', 1),
(41, 'Kocaeli', 1),
(42, 'Konya', 1),
(43, 'Kütahya', 1),
(44, 'Malatya', 1),
(45, 'Manisa', 1),
(46, 'Kahramanmaraş', 1),
(47, 'Mardin', 1),
(48, 'Muğla', 1),
(49, 'Muş', 1),
(50, 'Nevşehir', 1),
(51, 'Niğde', 1),
(52, 'Ordu', 1),
(53, 'Rize', 1),
(54, 'Sakarya', 1),
(55, 'Samsun', 1),
(56, 'Siirt', 1),
(57, 'Sinop', 1),
(58, 'Sivas', 1),
(59, 'Tekirdağ', 1),
(60, 'Tokat', 1),
(61, 'Trabzon', 1),
(62, 'Tunceli', 1),
(63, 'Şanlıurfa', 1),
(64, 'Uşak', 1),
(65, 'Van', 1),
(66, 'Yozgat', 1),
(67, 'Zonguldak', 1),
(68, 'Aksaray', 1),
(69, 'Bayburt', 1),
(70, 'Karaman', 1),
(71, 'Kırıkkale', 1),
(72, 'Batman', 1),
(73, 'Şırnak', 1),
(74, 'Bartın', 1),
(75, 'Ardahan', 1),
(76, 'Iğdır', 1),
(77, 'Yalova', 1),
(78, 'Karabük', 1),
(79, 'Kilis', 1),
(80, 'Osmaniye', 1),
(81, 'Düzce', 1);

-- --------------------------------------------------------

--
-- Table structure for table `countries`
--

CREATE TABLE `countries` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `countries`
--

INSERT INTO `countries` (`id`, `name`) VALUES
(1, 'Turkey'),
(3, 'UK'),
(4, 'USA');

-- --------------------------------------------------------

--
-- Table structure for table `difficulties`
--

CREATE TABLE `difficulties` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `difficulties`
--

INSERT INTO `difficulties` (`id`, `name`) VALUES
(1, 'Kolay'),
(2, 'Orta'),
(3, 'Zor');

-- --------------------------------------------------------

--
-- Table structure for table `games`
--

CREATE TABLE `games` (
  `id` int(11) NOT NULL,
  `date` datetime(6) NOT NULL,
  `score` double NOT NULL,
  `person_id` int(11) NOT NULL,
  `game_category_id` int(11) NOT NULL,
  `game_country_id` int(11) NOT NULL,
  `game_difficulty_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `id` int(11) NOT NULL,
  `title` varchar(300) NOT NULL,
  `difficultly_id` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  `city_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `questions`
--

INSERT INTO `questions` (`id`, `title`, `difficultly_id`, `category_id`, `city_id`) VALUES
(1, '06 nolu araç plakası kodu hangi şehre aittir?', 1, 1, 6),
(2, '42 nolu araç plakası kodu hangi şehre aittir?', 1, 1, 42),
(4, '45 nolu araç plakası kodu hangi şehre aittir?', 2, 1, 45),
(5, '34 nolu araç plakası kodu hangi şehre aittir?', 1, 1, 34),
(6, 'Çifte minareli medrese hangi ilimizdedir?', 2, 3, 25),
(7, 'Sard Antik Kent hangi ilimizdedir?', 2, 3, 45),
(8, 'Mevlana türbesi hangi ilimizdedir?', 1, 3, 42),
(9, 'Etli ekmek hangi ilimizin meşhur yemeğidir?', 1, 2, 42);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `Email` varchar(100) COLLATE utf8mb4_turkish_ci NOT NULL,
  `UserName` varchar(50) COLLATE utf8mb4_turkish_ci NOT NULL,
  `Password` varchar(20) COLLATE utf8mb4_turkish_ci NOT NULL,
  `name` varchar(50) COLLATE utf8mb4_turkish_ci DEFAULT NULL,
  `surname` varchar(50) COLLATE utf8mb4_turkish_ci DEFAULT NULL,
  `Score` bigint(20) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_turkish_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`ID`, `Email`, `UserName`, `Password`, `name`, `surname`, `Score`) VALUES
(12, '123@gmail.com', 'zuley', '1234', '', '', 60),
(13, '12345@gmail.com', 'zuley2', '1234', '', '', 0),
(14, 'zuleyhainl13@gmail.com', 'pupil2', 'yyyy', '', '', 0),
(16, '123@gmail', 'zuley13', 'z12345', '', '', 0),
(17, 'ert@ert.com', 'erturulla', '123456e', '', '', 0),
(18, '123xyz@gmail.com', 'zzzz', 'abC1234', NULL, NULL, 0),
(19, 'gaziedu@gmail.com', 'student', 'Abc1234', NULL, NULL, 0),
(20, 'aaaaaa@gmail.com', 'student2', 'Abc1234', NULL, NULL, 0),
(21, 'abcde@gmail.com', 'student3', 'Abc123', NULL, NULL, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`id`),
  ADD KEY `cuontry_id` (`cuontry_id`);

--
-- Indexes for table `countries`
--
ALTER TABLE `countries`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `difficulties`
--
ALTER TABLE `difficulties`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `games`
--
ALTER TABLE `games`
  ADD PRIMARY KEY (`id`),
  ADD KEY `person_id` (`person_id`),
  ADD KEY `game_category_id` (`game_category_id`),
  ADD KEY `game_country_id` (`game_country_id`),
  ADD KEY `game_difficulty_id` (`game_difficulty_id`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `category_id` (`category_id`),
  ADD KEY `city_id` (`city_id`),
  ADD KEY `difficultly_id` (`difficultly_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admins`
--
ALTER TABLE `admins`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `categories`
--
ALTER TABLE `categories`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `cities`
--
ALTER TABLE `cities`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=82;

--
-- AUTO_INCREMENT for table `countries`
--
ALTER TABLE `countries`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `difficulties`
--
ALTER TABLE `difficulties`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `games`
--
ALTER TABLE `games`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cities`
--
ALTER TABLE `cities`
  ADD CONSTRAINT `cities_ibfk_1` FOREIGN KEY (`cuontry_id`) REFERENCES `countries` (`id`);

--
-- Constraints for table `games`
--
ALTER TABLE `games`
  ADD CONSTRAINT `games_ibfk_1` FOREIGN KEY (`person_id`) REFERENCES `users` (`ID`),
  ADD CONSTRAINT `games_ibfk_2` FOREIGN KEY (`game_difficulty_id`) REFERENCES `difficulties` (`id`),
  ADD CONSTRAINT `games_ibfk_3` FOREIGN KEY (`game_country_id`) REFERENCES `countries` (`id`),
  ADD CONSTRAINT `games_ibfk_4` FOREIGN KEY (`game_category_id`) REFERENCES `categories` (`id`);

--
-- Constraints for table `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `questions_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`),
  ADD CONSTRAINT `questions_ibfk_2` FOREIGN KEY (`city_id`) REFERENCES `cities` (`id`),
  ADD CONSTRAINT `questions_ibfk_3` FOREIGN KEY (`difficultly_id`) REFERENCES `difficulties` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
