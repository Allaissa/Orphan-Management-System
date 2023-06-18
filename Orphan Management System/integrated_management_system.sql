-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 11, 2023 at 07:30 PM
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
  MODIFY `donation_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `employee`
--
ALTER TABLE `employee`
  MODIFY `employee_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `medical_history`
--
ALTER TABLE `medical_history`
  MODIFY `medical_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `medication_schedule`
--
ALTER TABLE `medication_schedule`
  MODIFY `medication_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `patients`
--
ALTER TABLE `patients`
  MODIFY `patient_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `payroll`
--
ALTER TABLE `payroll`
  MODIFY `payroll_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
