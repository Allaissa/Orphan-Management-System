-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 09, 2023 at 11:04 AM
-- Server version: 10.4.18-MariaDB
-- PHP Version: 7.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `integrated_management_system`
--

-- --------------------------------------------------------

--
-- Table structure for table `donations`
--

CREATE TABLE `donations` (
  `donation_id` int(11) NOT NULL,
  `sponsor_name` varchar(60) NOT NULL,
  `sponsor_gender` varchar(60) NOT NULL,
  `sponsor_address` varchar(150) NOT NULL,
  `donation_phone_number` varchar(20) NOT NULL,
  `donation_date_donated` varchar(60) NOT NULL,
  `donation_type` varchar(60) NOT NULL,
  `inventory_type` varchar(60) NOT NULL,
  `cash_amount` int(11) NOT NULL,
  `donation_quantity` varchar(20) NOT NULL,
  `donation_created_at` timestamp NULL DEFAULT NULL,
  `donation_updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `donations`
--

INSERT INTO `donations` (`donation_id`, `sponsor_name`, `sponsor_gender`, `sponsor_address`, `donation_phone_number`, `donation_date_donated`, `donation_type`, `inventory_type`, `cash_amount`, `donation_quantity`, `donation_created_at`, `donation_updated_at`) VALUES
(22, 'July 2023', 'Male', 'sada', '11111111111', '01/07/23', 'Cash', 'N/A', 421, '0', '2023-06-06 16:07:38', '2023-06-06 16:09:46'),
(23, 'July 2024', 'Male', 'sda', '11111111111', '01/07/24', 'Cash', 'N/A', 23131, '0', '2023-06-06 16:08:06', '2023-06-06 16:09:53'),
(24, 'July 2025', 'Male', 'sad', '11111111111', '01/07/25', 'Cash', 'N/A', 3131, '0', '2023-06-06 16:08:18', '2023-06-06 16:09:58'),
(25, 'Jan 2025', 'Male', 'sda', '11111111111', '01/01/25', 'Cash', 'N/A', 2131, '0', '2023-06-06 16:08:36', '2023-06-06 16:12:16'),
(26, 'Jan 2024', 'Male', 'sad', '11111111111', '01/01/24', 'Cash', 'N/A', 1221, '0', '2023-06-06 16:08:50', '2023-06-06 16:10:12'),
(27, 'Jan 2023', 'Male', 'sda', '11111111111', '01/01/23', 'Cash', 'N/A', 21312, '0', '2023-06-06 16:08:59', '2023-06-06 16:10:17'),
(28, 'Feb 2023', 'Male', 'sca', '11111111111', '01/02/23', 'Cash', 'N/A', 213, '0', '2023-06-06 16:09:16', NULL),
(29, 'Feb 2024', 'Male', 'sda', '11111111111', '01/02/24', 'Cash', 'N/A', 21312, '0', '2023-06-06 16:09:28', NULL),
(30, 'Feb 2025', 'Male', 'sda', '11111111111', '01/02/25', 'Cash', 'N/A', 2131223, '0', '2023-06-06 16:09:39', NULL),
(31, 'Octo 2023', 'Male', 'sadad', '11111111111', '11/10/23', 'Cash', 'N/A', 213124, '0', '2023-06-09 01:42:52', NULL),
(32, 'Jayvie', 'Male', 'sda', '11111111111', '09/01/23', 'Cash', 'N/A', 231323, '0', '2023-06-09 08:37:48', NULL),
(33, 'sda', 'Male', 'dsa', '11111111111', '09/02/23', 'Cash', 'N/A', 2313, '0', '2023-06-09 08:38:00', NULL),
(34, 'cess', 'Male', 'sad', '11111111111', '09/03/23', 'Cash', 'N/A', 231, '0', '2023-06-09 08:38:19', NULL),
(35, 'zed', 'Male', 'sda', '11111111111', '09/04/23', 'Cash', 'N/A', 12, '0', '2023-06-09 08:38:30', NULL),
(36, 'sada', 'Male', 'ada', '11111111111', '09/05/23', 'Cash', 'N/A', 21, '0', '2023-06-09 08:38:47', NULL),
(37, 'jay', 'Male', 'dsadsa', '11111111111', '09/06/23', 'Cash', 'N/A', 421, '0', '2023-06-09 08:39:04', NULL),
(38, 'jorge', 'Male', 'dsa', '11111111111', '09/07/23', 'Cash', 'N/A', 2341, '0', '2023-06-09 08:39:22', NULL),
(39, 'marcy', 'Female', 'dsa', '11111111111', '09/08/23', 'Cash', 'N/A', 231, '0', '2023-06-09 08:39:41', NULL),
(40, 'lucas', 'Male', 'sda', '11111111111', '09/09/23', 'Cash', 'N/A', 231, '0', '2023-06-09 08:39:57', NULL),
(41, 'mabel', 'Female', 'dsa', '11111111111', '09/10/23', 'Cash', 'N/A', 321, '0', '2023-06-09 08:40:06', NULL),
(42, 'lunox', 'Female', 'dsa', '11111111111', '09/11/23', 'Cash', 'N/A', 231, '0', '2023-06-09 08:40:15', NULL),
(43, 'sharon', 'Female', 'dsa', '11111111111', '09/12/23', 'Cash', 'N/A', 2131, '0', '2023-06-09 08:40:24', NULL),
(44, 'francis', 'Male', 'sad', '11111111111', '09/01/24', 'Cash', 'N/A', 231, '0', '2023-06-09 08:42:26', NULL),
(45, 'francec', 'Female', 'dsa', '11111111111', '09/02/24', 'Cash', 'N/A', 231, '0', '2023-06-09 08:42:36', NULL),
(46, 'chesca', 'Female', 'dsa', '11111111111', '09/03/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:42:53', NULL),
(47, 'cesca', 'Female', 'dsa', '11111111111', '09/04/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:43:07', NULL),
(48, 'kkj', 'Female', 'dsa', '11111111111', '09/05/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:43:17', NULL),
(49, 'alisa', 'Female', 'sad', '11111111111', '09/06/24', 'Cash', 'N/A', 231, '0', '2023-06-09 08:43:37', NULL),
(50, 'yoru', 'Male', 'dsa', '11111111111', '09/07/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:43:48', NULL),
(51, 'ur', 'Male', 'sda', '11111111111', '09/08/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:43:56', NULL),
(52, 'kilaa', 'Male', 'dsa', '11111111111', '09/09/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:44:05', NULL),
(53, 'hau', 'Female', 'sda', '11111111111', '09/10/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:44:13', NULL),
(54, 'zeraa', 'Male', 'sda', '11111111111', '09/11/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:44:19', NULL),
(55, 'nadel', 'Male', 'dsa', '11111111111', '09/12/24', 'Cash', 'N/A', 0, '0', '2023-06-09 08:44:27', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

CREATE TABLE `employee` (
  `employee_id` int(11) NOT NULL,
  `employee_image` mediumblob DEFAULT NULL,
  `employee_first_name` varchar(60) NOT NULL,
  `employee_middle_name` varchar(60) NOT NULL,
  `employee_last_name` varchar(60) NOT NULL,
  `employee_fullname` varchar(50) NOT NULL,
  `employee_gender` varchar(60) NOT NULL,
  `employee_contact_number` varchar(20) NOT NULL,
  `employee_address` varchar(150) NOT NULL,
  `employee_designation` varchar(60) NOT NULL,
  `employee_birthday` varchar(50) NOT NULL,
  `employee_religion` varchar(50) NOT NULL,
  `employee_educational` varchar(100) NOT NULL,
  `employee_status` varchar(10) NOT NULL,
  `employee_created_at` timestamp NULL DEFAULT NULL,
  `employee_updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`employee_id`, `employee_image`, `employee_first_name`, `employee_middle_name`, `employee_last_name`, `employee_fullname`, `employee_gender`, `employee_contact_number`, `employee_address`, `employee_designation`, `employee_birthday`, `employee_religion`, `employee_educational`, `employee_status`, `employee_created_at`, `employee_updated_at`) VALUES
(1, NULL, 'Jayvie', 'sffferrer', 'De Leon', 'Jayvie sffferrer De Leon', 'Female', '11111111111', 'ada', 'sada', '28/05/23', 'adad', 'sda', 'Single', '2023-05-28 08:20:02', '2023-05-28 10:16:17'),
(2, NULL, 'Alissa', 'sada', 'Medina', 'Alissa sada Medina', 'Male', '11111111111', 'dsadas', 'sdaada', '28/05/23', 'dsa', 'sdasds', 'Single', '2023-05-28 08:56:40', '2023-05-28 10:16:01');

-- --------------------------------------------------------

--
-- Table structure for table `medical_history`
--

CREATE TABLE `medical_history` (
  `medical_id` int(11) NOT NULL,
  `medical_name` varchar(60) NOT NULL,
  `medical_diagnostic` varchar(60) NOT NULL,
  `medical_intake` varchar(60) NOT NULL,
  `medical_blood_pressure` varchar(60) NOT NULL,
  `medical_temperature` varchar(4) NOT NULL,
  `medical_weight` varchar(60) NOT NULL,
  `medical_height` varchar(60) NOT NULL,
  `medical_doctors_name` varchar(60) NOT NULL,
  `medical_date_recorded` varchar(60) NOT NULL,
  `medical_created_at` timestamp NULL DEFAULT NULL,
  `medical_updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `medical_history`
--

INSERT INTO `medical_history` (`medical_id`, `medical_name`, `medical_diagnostic`, `medical_intake`, `medical_blood_pressure`, `medical_temperature`, `medical_weight`, `medical_height`, `medical_doctors_name`, `medical_date_recorded`, `medical_created_at`, `medical_updated_at`) VALUES
(1, 'Jayvie De Leon', 'Cold', 'Neozep', '120/80', '', '32 KG', '3\'2 FT', 'sdad', '05/06/23', '2023-05-28 05:42:11', '2023-06-05 08:41:37'),
(2, 'Jayvie De Leon', 'dsada', 'sadaa', 'sda', '', '44 KG', '4\'2  FT', 'sad', '05/06/23', '2023-05-28 06:11:46', '2023-06-05 08:40:26'),
(3, 'Allaissa Medina', 'Fever', 'Biogesic', '120/80', '', '45 KG', '5\'3 FT', 'Crab', '28/05/23', '2023-05-28 10:14:06', NULL),
(4, 'Allaissa Medina', 'sad', '34 °', 'sadas', '33', 'sadas KG', 'adas FT', 'sad', '05/06/23', '2023-06-05 08:50:48', '2023-06-05 09:07:37'),
(5, 'Jayvie De Leon', 'sad', 'sad', 'sad', '32 °', 'sad KG', 'sad FT', '23', '05/06/23', '2023-06-05 09:07:19', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `medication_schedule`
--

CREATE TABLE `medication_schedule` (
  `medication_id` int(11) NOT NULL,
  `medication_medicine_name` varchar(60) DEFAULT NULL,
  `medication_dosage` varchar(60) DEFAULT NULL,
  `medication_stock` varchar(60) DEFAULT NULL,
  `medication_created_at` date DEFAULT NULL,
  `medication_updated_at` date DEFAULT NULL,
  `medication_expiration_date` varchar(60) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Table structure for table `patients`
--

CREATE TABLE `patients` (
  `patient_id` int(11) NOT NULL,
  `patient_first_name` varchar(60) NOT NULL,
  `patient_middle_name` varchar(60) NOT NULL,
  `patient_last_name` varchar(60) NOT NULL,
  `patient_fullname` varchar(50) NOT NULL,
  `patient_orphan_address` varchar(250) NOT NULL,
  `patient_birth_date` varchar(250) NOT NULL,
  `patient_place_of_birth` varchar(250) NOT NULL,
  `patient_status` varchar(60) NOT NULL,
  `patient_religion` varchar(50) NOT NULL,
  `patient_educational_attainment` varchar(250) NOT NULL,
  `patient_family_member_name` varchar(100) NOT NULL,
  `patient_relation_to_client` varchar(250) NOT NULL,
  `patient_family_address` varchar(250) NOT NULL,
  `patient_emergency_number` varchar(20) NOT NULL,
  `patient_description` varchar(500) NOT NULL,
  `patient_date_of_admission` varchar(50) NOT NULL,
  `patient_created_at` timestamp NULL DEFAULT NULL,
  `patient_updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `patients`
--

INSERT INTO `patients` (`patient_id`, `patient_first_name`, `patient_middle_name`, `patient_last_name`, `patient_fullname`, `patient_orphan_address`, `patient_birth_date`, `patient_place_of_birth`, `patient_status`, `patient_religion`, `patient_educational_attainment`, `patient_family_member_name`, `patient_relation_to_client`, `patient_family_address`, `patient_emergency_number`, `patient_description`, `patient_date_of_admission`, `patient_created_at`, `patient_updated_at`) VALUES
(1, 'Jayvie', 'Ferrer', 'De Leon', 'Jayvie Ferrer De Leon', 'sadad', '28/05/23', 'sdada', 'Married', 'sad', 'sadadsa', 'sdadas', 'dsa', 'sadas', '11111111111', 'sadsas', '28/05/23', '2023-05-28 05:41:56', '2023-05-28 10:03:45'),
(2, 'Ben10', 'sdad', 'sada', 'Ben10 sdad sada', 'sada', '28/05/23', 'sda', 'Widowed', 'da', 'sada', 'sadd', 'a', 'sadas', '11111111111', 'sadas', '28/05/23', '2023-05-28 05:42:50', '2023-05-28 10:07:01'),
(5, 'Allaissa', 'sad', 'Medina', 'Allaissa sad Medina', 'adassdsa', '28/05/23', 'sad', 'Single', 'dad', 'assad', 'sda', 'sad', 'asda', '11111111111', 'asdsa', '28/05/23', '2023-05-28 10:13:26', '2023-05-28 10:13:38');

-- --------------------------------------------------------

--
-- Table structure for table `payroll`
--

CREATE TABLE `payroll` (
  `payroll_id` int(11) NOT NULL,
  `user_id` varchar(50) NOT NULL,
  `basic_salary` decimal(10,0) NOT NULL,
  `number_of_days` int(11) NOT NULL,
  `total_salary` int(11) NOT NULL,
  `payroll_created_at` date DEFAULT NULL,
  `payroll_updated_at` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `payroll`
--

INSERT INTO `payroll` (`payroll_id`, `user_id`, `basic_salary`, `number_of_days`, `total_salary`, `payroll_created_at`, `payroll_updated_at`) VALUES
(1, 'Alissa Medina', '500', 15, 7500, '2023-05-28', '2023-05-28'),
(2, 'Alissa Medina', '1000', 15, 15000, '2023-05-28', NULL),
(3, 'Jayvie De Leon', '432', 234, 101088, '2023-06-09', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(60) DEFAULT NULL,
  `user_password` varchar(60) DEFAULT NULL,
  `user_created_at` datetime DEFAULT NULL,
  `user_updated_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `user_name`, `user_password`, `user_created_at`, `user_updated_at`) VALUES
(10, 'Allaissa', '1234', '2023-04-06 13:14:06', '2023-05-24 00:43:26'),
(12, 'Maria', '1234', '2023-04-08 22:16:28', '2023-04-16 06:46:33'),
(19, 'Tine', '1234', '2023-04-16 04:51:26', '2023-04-16 06:46:22'),
(20, 'Glayza', '1234', '2023-04-16 06:46:07', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `donations`
--
ALTER TABLE `donations`
  ADD PRIMARY KEY (`donation_id`);

--
-- Indexes for table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`employee_id`);

--
-- Indexes for table `medical_history`
--
ALTER TABLE `medical_history`
  ADD PRIMARY KEY (`medical_id`);

--
-- Indexes for table `medication_schedule`
--
ALTER TABLE `medication_schedule`
  ADD PRIMARY KEY (`medication_id`);

--
-- Indexes for table `patients`
--
ALTER TABLE `patients`
  ADD PRIMARY KEY (`patient_id`);

--
-- Indexes for table `payroll`
--
ALTER TABLE `payroll`
  ADD PRIMARY KEY (`payroll_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `donations`
--
ALTER TABLE `donations`
  MODIFY `donation_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- AUTO_INCREMENT for table `employee`
--
ALTER TABLE `employee`
  MODIFY `employee_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `medical_history`
--
ALTER TABLE `medical_history`
  MODIFY `medical_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `medication_schedule`
--
ALTER TABLE `medication_schedule`
  MODIFY `medication_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `patients`
--
ALTER TABLE `patients`
  MODIFY `patient_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `payroll`
--
ALTER TABLE `payroll`
  MODIFY `payroll_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
