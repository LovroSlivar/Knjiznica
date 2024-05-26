CREATE DATABASE  IF NOT EXISTS `knjiznica` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `knjiznica`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: knjiznica
-- ------------------------------------------------------
-- Server version	8.4.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240515175854_Initial','7.0.18');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroleclaims`
--

DROP TABLE IF EXISTS `aspnetroleclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroleclaims`
--

LOCK TABLES `aspnetroleclaims` WRITE;
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetroles`
--

DROP TABLE IF EXISTS `aspnetroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetroles`
--

LOCK TABLES `aspnetroles` WRITE;
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
INSERT INTO `aspnetroles` VALUES ('765742fa-a49e-4d9f-bb75-476a93ff2fee','User','USER',NULL),('f05a0bc0-3772-43c4-b75d-8ac7811520a0','Admin','ADMIN',NULL);
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserclaims`
--

DROP TABLE IF EXISTS `aspnetuserclaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserclaims`
--

LOCK TABLES `aspnetuserclaims` WRITE;
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserlogins`
--

DROP TABLE IF EXISTS `aspnetuserlogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserlogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserlogins`
--

LOCK TABLES `aspnetuserlogins` WRITE;
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetuserroles`
--

DROP TABLE IF EXISTS `aspnetuserroles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetuserroles`
--

LOCK TABLES `aspnetuserroles` WRITE;
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
INSERT INTO `aspnetuserroles` VALUES ('2bb39b3c-0b5e-4401-af00-1b871e1c1197','765742fa-a49e-4d9f-bb75-476a93ff2fee'),('8143ba5d-6404-45a3-9ed2-510c7bf78d5f','765742fa-a49e-4d9f-bb75-476a93ff2fee'),('c4ac66b8-c130-4463-8f11-505a732d3007','765742fa-a49e-4d9f-bb75-476a93ff2fee'),('c4ac66b8-c130-4463-8f11-505a732d3007','f05a0bc0-3772-43c4-b75d-8ac7811520a0');
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusers`
--

DROP TABLE IF EXISTS `aspnetusers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusers`
--

LOCK TABLES `aspnetusers` WRITE;
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` VALUES ('2bb39b3c-0b5e-4401-af00-1b871e1c1197','josipa.fabus@gmail.com','JOSIPA.FABUS@GMAIL.COM','josipa.fabus@gmail.com','JOSIPA.FABUS@GMAIL.COM',0,'AQAAAAIAAYagAAAAEP/Fabta5P47CC3PZj1tzO9mxrkkY0YlmxiSfO8LfvlsYiz8eW4n4gUj5autYy4asA==','EQ75EEW2PAMWENZ5JJ32RRF7JEXXI4QT','f8fe40ac-619a-4184-89ef-e4d58b26bbb7',NULL,0,0,NULL,1,0),('8143ba5d-6404-45a3-9ed2-510c7bf78d5f','lovroslivar@gmail.com','LOVROSLIVAR@GMAIL.COM','lovroslivar@gmail.com','LOVROSLIVAR@GMAIL.COM',0,'AQAAAAIAAYagAAAAEPd8UpHTNPSsGNbYFJp5Q4Q0WePO4ut4Aebd8k4/JPfKf88BJ9x5Nj0ko+0FOGAAIA==','3EY374YQFYH4BBDN7CEN5JNC7C2DJK6N','a0cba366-affc-4463-bd81-29edbb6a160c',NULL,0,0,NULL,1,0),('c4ac66b8-c130-4463-8f11-505a732d3007','lovro.slivar@gmail.com','LOVRO.SLIVAR@GMAIL.COM','lovro.slivar@gmail.com','LOVRO.SLIVAR@GMAIL.COM',0,'AQAAAAIAAYagAAAAECTEsk1hiYxHqgpENW+E+zRZcClyWLRKZ3KIYIC8Tuj2tb9Zuxxmzzqx0/hGdoEOoQ==','JFS2ZRT5EKXHVZQFXJKXKO3STFECCLAS','f58a71e4-29b0-4974-83e0-0f79b63e1de9',NULL,0,0,NULL,1,0);
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aspnetusertokens`
--

DROP TABLE IF EXISTS `aspnetusertokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aspnetusertokens`
--

LOCK TABLES `aspnetusertokens` WRITE;
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `autor`
--

DROP TABLE IF EXISTS `autor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `autor` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `ime_prezime` varchar(50) NOT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `autor`
--

LOCK TABLES `autor` WRITE;
/*!40000 ALTER TABLE `autor` DISABLE KEYS */;
INSERT INTO `autor` VALUES (1,'Ivo Andrić',1),(2,'Miroslav Krleža',1),(3,'Marija Jurić Zagorka',1),(4,'Ivana Brlić-Mažuranić',1),(5,'Antun Gustav Matoš',1),(6,'August Šenoa',1),(7,'Tin Ujević',1),(8,'Ranko Marinković',1),(9,'Slavko Kolar',1),(10,'Pavao Pavličić',1),(11,'Ante Tomić',1),(12,'Arsen Dedić',1),(13,'Damir Karakaš',1),(14,'Slavenka Drakulić',1),(15,'Dubravka Ugrešić',1),(16,'Ivica Prtenjača',1),(17,'Kristian Novak',1),(18,'Davor Špišić',1),(19,'Miljenko Jergović',1),(20,'Julijana Matanović',1);
/*!40000 ALTER TABLE `autor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dogadanje`
--

DROP TABLE IF EXISTS `dogadanje`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dogadanje` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `knjigaID` int DEFAULT NULL,
  `prikaz_od` date DEFAULT NULL,
  `prokaz_do` date DEFAULT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `knjigaID` (`knjigaID`),
  CONSTRAINT `dogadanje_ibfk_1` FOREIGN KEY (`knjigaID`) REFERENCES `knjiga` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dogadanje`
--

LOCK TABLES `dogadanje` WRITE;
/*!40000 ALTER TABLE `dogadanje` DISABLE KEYS */;
INSERT INTO `dogadanje` VALUES (21,36,'2024-05-20','2024-05-30',1),(22,37,'2024-05-21','2024-05-31',1),(23,38,'2024-05-22','2024-06-01',1),(24,39,'2024-05-23','2024-06-02',1),(25,40,'2024-05-24','2024-06-03',1),(26,41,'2024-05-25','2024-06-04',1),(27,42,'2024-05-26','2024-06-05',1),(28,43,'2024-05-27','2024-06-06',1),(29,44,'2024-05-28','2024-06-07',1),(30,45,'2024-05-29','2024-06-08',1),(31,46,'2024-05-30','2024-06-09',1),(32,47,'2024-05-31','2024-06-10',1),(33,48,'2024-06-01','2024-06-11',1),(34,49,'2024-06-02','2024-06-12',1),(35,50,'2024-06-03','2024-06-13',1),(36,51,'2024-06-04','2024-06-14',1),(37,52,'2024-06-05','2024-06-15',1),(38,53,'2024-06-06','2024-06-16',1),(39,54,'2024-06-07','2024-06-17',1),(40,55,'2024-06-08','2024-06-18',1);
/*!40000 ALTER TABLE `dogadanje` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jezik`
--

DROP TABLE IF EXISTS `jezik`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jezik` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `naziv` varchar(50) NOT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jezik`
--

LOCK TABLES `jezik` WRITE;
/*!40000 ALTER TABLE `jezik` DISABLE KEYS */;
INSERT INTO `jezik` VALUES (1,'Hrvatski',1),(2,'Engleski',1),(3,'Njemački',1),(4,'Francuski',1),(5,'Španjolski',1),(6,'Talijanski',1),(7,'Ruski',1),(8,'Kineski',1),(9,'Japanski',1),(10,'Portugalski',1),(11,'Grčki',1),(12,'Latinski',1),(13,'Mađarski',1),(14,'Češki',1),(15,'Slovački',1),(16,'Poljski',1),(17,'Švedski',1),(18,'Norveški',1),(19,'Danski',1),(20,'Finski',1);
/*!40000 ALTER TABLE `jezik` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `knjiga`
--

DROP TABLE IF EXISTS `knjiga`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `knjiga` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `naslov` varchar(50) NOT NULL,
  `opis` varchar(200) DEFAULT NULL,
  `godina_izdavanja` int DEFAULT NULL,
  `broj_stranica` int DEFAULT NULL,
  `zanrID` int DEFAULT NULL,
  `jezikID` int DEFAULT NULL,
  `uzrastID` int DEFAULT NULL,
  `autorID` int DEFAULT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `zanrID` (`zanrID`),
  KEY `jezikID` (`jezikID`),
  KEY `uzrastID` (`uzrastID`),
  KEY `autorID` (`autorID`),
  CONSTRAINT `knjiga_ibfk_1` FOREIGN KEY (`zanrID`) REFERENCES `zanr` (`ID`),
  CONSTRAINT `knjiga_ibfk_2` FOREIGN KEY (`jezikID`) REFERENCES `jezik` (`ID`),
  CONSTRAINT `knjiga_ibfk_3` FOREIGN KEY (`uzrastID`) REFERENCES `uzrast` (`ID`),
  CONSTRAINT `knjiga_ibfk_4` FOREIGN KEY (`autorID`) REFERENCES `autor` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `knjiga`
--

LOCK TABLES `knjiga` WRITE;
/*!40000 ALTER TABLE `knjiga` DISABLE KEYS */;
INSERT INTO `knjiga` VALUES (36,'Lolita','Drama o moralnim dilemama',1955,400,1,1,1,1,1),(37,'1984','Distopijski roman',1949,320,2,2,2,2,1),(38,'Lovac u žitu','Društveni roman',1951,280,3,3,3,3,1),(39,'Na zapadu ništa novo','Antiratni roman',1928,330,4,4,4,4,1),(40,'Rat i mir','Povijesni roman o Napoleonu',1869,1000,5,5,5,5,1),(41,'Mačka u vreći','Misteriozni triler',2015,280,6,6,6,6,1),(42,'Gola istina','Autobiografija slavnog sportaša',2009,360,7,7,7,7,1),(43,'Mrtvo more','Misteriozni roman',2017,320,8,8,8,8,1),(44,'Lovci na zmajeve','Roman o prijateljstvu i hrabrosti',2003,400,9,9,9,9,1),(45,'Buka i bijes','Obiteljska saga',1952,290,10,10,10,10,1),(46,'Proces','Filozofski roman',1925,250,11,11,11,11,1),(47,'Alkemičar','Fantastični roman',1988,200,12,12,12,12,1),(48,'Zločin i kazna','Psihološki roman',1866,430,13,13,13,13,1),(49,'Harry Potter i Kamen mudraca','Fantazijski roman',1997,320,14,14,14,14,1),(50,'Ana Karenjina','Ljubavni roman',1878,800,15,15,15,15,1),(51,'Narodni ustanak u Irskoj','Povijesni roman',1916,370,16,16,16,16,1),(52,'Veliki Gatsby','Američka književnost',1925,200,17,17,17,17,1),(53,'Oliver Twist','Društveni roman',1837,500,18,18,18,18,1),(54,'Mali princ','Dječji roman',1943,100,19,19,19,19,1),(55,'Veličanstveni gospodin Ripley','Kriminalistički roman',1955,300,20,20,20,20,1),(56,'Homer','dsakdakjlsd dasda sdkasasdkaskdla',1990,200,5,14,16,6,1);
/*!40000 ALTER TABLE `knjiga` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lager`
--

DROP TABLE IF EXISTS `lager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lager` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `knjigaID` int DEFAULT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `knjigaID` (`knjigaID`),
  CONSTRAINT `lager_ibfk_1` FOREIGN KEY (`knjigaID`) REFERENCES `knjiga` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=42 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lager`
--

LOCK TABLES `lager` WRITE;
/*!40000 ALTER TABLE `lager` DISABLE KEYS */;
INSERT INTO `lager` VALUES (21,36,0),(22,36,1),(23,37,1),(24,36,1),(25,37,1),(26,42,1),(27,42,1),(28,47,1),(29,43,1),(30,38,1),(31,41,1),(32,43,1),(33,46,1),(34,39,1),(35,45,1),(36,43,1),(37,41,1),(38,40,1),(39,38,1),(40,39,1),(41,56,1);
/*!40000 ALTER TABLE `lager` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recenzija`
--

DROP TABLE IF EXISTS `recenzija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recenzija` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `knjigaID` int DEFAULT NULL,
  `bodovi` int DEFAULT NULL,
  `aktivan` int DEFAULT NULL,
  `userID` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `knjigaID` (`knjigaID`),
  KEY `userID` (`userID`),
  CONSTRAINT `recenzija_ibfk_1` FOREIGN KEY (`knjigaID`) REFERENCES `knjiga` (`ID`),
  CONSTRAINT `recenzija_ibfk_2` FOREIGN KEY (`userID`) REFERENCES `aspnetusers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recenzija`
--

LOCK TABLES `recenzija` WRITE;
/*!40000 ALTER TABLE `recenzija` DISABLE KEYS */;
INSERT INTO `recenzija` VALUES (41,36,4,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(42,37,5,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(43,43,3,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(44,42,5,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(45,40,4,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(46,45,5,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(47,36,4,NULL,'2bb39b3c-0b5e-4401-af00-1b871e1c1197'),(48,38,3,NULL,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f');
/*!40000 ALTER TABLE `recenzija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rezervacija`
--

DROP TABLE IF EXISTS `rezervacija`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rezervacija` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `lagerID` int DEFAULT NULL,
  `posudba_od` date DEFAULT NULL,
  `posudba_do` date DEFAULT NULL,
  `aktivan` int DEFAULT NULL,
  `userID` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `lagerID` (`lagerID`),
  KEY `userID` (`userID`),
  CONSTRAINT `rezervacija_ibfk_1` FOREIGN KEY (`lagerID`) REFERENCES `lager` (`ID`),
  CONSTRAINT `rezervacija_ibfk_2` FOREIGN KEY (`userID`) REFERENCES `aspnetusers` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=79 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rezervacija`
--

LOCK TABLES `rezervacija` WRITE;
/*!40000 ALTER TABLE `rezervacija` DISABLE KEYS */;
INSERT INTO `rezervacija` VALUES (61,21,'2024-05-20','2024-06-19',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(62,21,'2024-05-20','2024-06-19',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(63,21,'2024-05-20','2024-06-19',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(64,22,'2024-05-20','2024-06-19',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(65,23,'2024-05-20','2024-06-19',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(66,24,'2024-05-20','2024-06-19',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(69,21,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(70,22,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(71,22,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(72,22,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(73,29,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(74,26,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(75,38,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(76,35,'2024-05-21','2024-06-20',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f'),(77,22,'2024-05-23','2024-06-22',0,'2bb39b3c-0b5e-4401-af00-1b871e1c1197'),(78,30,'2024-05-24','2024-06-23',0,'8143ba5d-6404-45a3-9ed2-510c7bf78d5f');
/*!40000 ALTER TABLE `rezervacija` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `uzrast`
--

DROP TABLE IF EXISTS `uzrast`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `uzrast` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `naziv` varchar(50) NOT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `uzrast`
--

LOCK TABLES `uzrast` WRITE;
/*!40000 ALTER TABLE `uzrast` DISABLE KEYS */;
INSERT INTO `uzrast` VALUES (1,'Dječji',1),(2,'Tinejdžeri',1),(3,'Odrasli',1),(4,'Svi uzrasti',1),(5,'Stariji',1),(6,'Djeca',1),(7,'Mladi',1),(8,'Srednja škola',1),(9,'Fakultet',1),(10,'Stariji odrasli',1),(11,'Mlađi odrasli',1),(12,'Djeca i odrasli',1),(13,'Tinejdžeri i odrasli',1),(14,'Svi uzrasti i odrasli',1),(15,'Djeca i tinejdžeri',1),(16,'Djeca, tinejdžeri i odrasli',1),(17,'Djeca i stariji',1),(18,'Mladi i odrasli',1),(19,'Djeca i mlađi odrasli',1),(20,'Djeca i srednja škola',1);
/*!40000 ALTER TABLE `uzrast` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `zanr`
--

DROP TABLE IF EXISTS `zanr`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `zanr` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `naziv` varchar(50) NOT NULL,
  `aktivan` int DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `zanr`
--

LOCK TABLES `zanr` WRITE;
/*!40000 ALTER TABLE `zanr` DISABLE KEYS */;
INSERT INTO `zanr` VALUES (1,'Fikcija',1),(2,'Povijest',1),(3,'Biografija',1),(4,'Triler',1),(5,'Misterija',1),(6,'Romansa',1),(7,'Sci-Fi',1),(8,'Fantazija',1),(9,'Horor',1),(10,'Drama',1),(11,'Avantura',1),(12,'Komedija',1),(13,'Autobiografija',1),(14,'Esej',1),(15,'Poema',1),(16,'Satira',1),(17,'Dnevnik',1),(18,'Putopis',1),(19,'Mitologija',1),(20,'Lektira',1);
/*!40000 ALTER TABLE `zanr` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-26 23:09:07
