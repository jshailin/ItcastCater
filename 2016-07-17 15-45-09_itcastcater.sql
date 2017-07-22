/*
SQLyog Ultimate v12.09 (32 bit)
MySQL - 5.6.28-log : Database - itcastcater
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`ItcastCater` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `ItcastCater`;

/*Table structure for table `DishInfo` */

DROP TABLE IF EXISTS `DishInfo`;

CREATE TABLE `DishInfo` (
  `DId` int(11) NOT NULL AUTO_INCREMENT,
  `DTitle` varchar(10) DEFAULT NULL,
  `DTypeId` int(11) DEFAULT NULL,
  `DPrice` decimal(5,2) DEFAULT NULL,
  `DChar` varchar(10) DEFAULT NULL,
  `DIsDelete` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`DId`),
  KEY `DTypeId` (`DTypeId`),
  CONSTRAINT `DishInfo_ibfk_1` FOREIGN KEY (`DTypeId`) REFERENCES `DishTypeInfo` (`DId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `DishInfo` */

insert  into `DishInfo`(`DId`,`DTitle`,`DTypeId`,`DPrice`,`DChar`,`DIsDelete`) values (1,'梅菜扣肉',1,'30.00','MCKR',0),(2,'猪肉炖粉条',4,'20.00','ZRDFT',0);

/*Table structure for table `DishTypeInfo` */

DROP TABLE IF EXISTS `DishTypeInfo`;

CREATE TABLE `DishTypeInfo` (
  `DId` int(11) NOT NULL AUTO_INCREMENT,
  `DTitle` varchar(10) DEFAULT NULL,
  `DIsDelete` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`DId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `DishTypeInfo` */

insert  into `DishTypeInfo`(`DId`,`DTitle`,`DIsDelete`) values (1,'湘菜',0),(2,'粤菜',0),(3,'川菜',0),(4,'东北菜',0);

/*Table structure for table `HallInfo` */

DROP TABLE IF EXISTS `HallInfo`;

CREATE TABLE `HallInfo` (
  `HId` int(11) NOT NULL AUTO_INCREMENT,
  `HTitle` varchar(10) DEFAULT NULL,
  `HIsDelete` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`HId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*Data for the table `HallInfo` */

insert  into `HallInfo`(`HId`,`HTitle`,`HIsDelete`) values (1,'昆仑厅',0),(2,'太行厅',0),(3,'包房',0);

/*Table structure for table `ManagerInfo` */

DROP TABLE IF EXISTS `ManagerInfo`;

CREATE TABLE `ManagerInfo` (
  `MId` int(11) NOT NULL AUTO_INCREMENT,
  `MName` varchar(10) DEFAULT NULL,
  `MPwd` varchar(32) DEFAULT NULL,
  `MType` int(11) DEFAULT NULL,
  PRIMARY KEY (`MId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*Data for the table `ManagerInfo` */

insert  into `ManagerInfo`(`MId`,`MName`,`MPwd`,`MType`) values (1,'dsm','202cb962ac59075b964b07152d234b70',1),(2,'clx','202cb962ac59075b964b07152d234b70',0);

/*Table structure for table `MemberInfo` */

DROP TABLE IF EXISTS `MemberInfo`;

CREATE TABLE `MemberInfo` (
  `MId` int(11) NOT NULL AUTO_INCREMENT,
  `MTypeId` int(11) DEFAULT NULL,
  `MName` varchar(10) DEFAULT NULL,
  `MPhone` varchar(11) DEFAULT NULL,
  `MMoney` decimal(6,2) DEFAULT NULL,
  `MIsDelete` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`MId`),
  KEY `MTypeId` (`MTypeId`),
  CONSTRAINT `MemberInfo_ibfk_1` FOREIGN KEY (`MTypeId`) REFERENCES `MemberTypeInfo` (`MId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `MemberInfo` */

insert  into `MemberInfo`(`MId`,`MTypeId`,`MName`,`MPhone`,`MMoney`,`MIsDelete`) values (1,3,'c12','12345678901','1000.00',0),(2,2,'c11','12345617890','1952.00',0),(3,4,'c13','23456789091','300.00',0),(4,1,'c14','98765432109','9999.99',0);

/*Table structure for table `MemberTypeInfo` */

DROP TABLE IF EXISTS `MemberTypeInfo`;

CREATE TABLE `MemberTypeInfo` (
  `MId` int(11) NOT NULL AUTO_INCREMENT,
  `MTitle` varchar(10) DEFAULT NULL,
  `MDiscount` decimal(3,2) DEFAULT NULL,
  `MIsDelete` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`MId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Data for the table `MemberTypeInfo` */

insert  into `MemberTypeInfo`(`MId`,`MTitle`,`MDiscount`,`MIsDelete`) values (1,'钻石会员','0.70',0),(2,'白金会员','0.80',0),(3,'黄金会员','0.90',0),(4,'普通会员','0.98',0);

/*Table structure for table `OrderDetailInfo` */

DROP TABLE IF EXISTS `OrderDetailInfo`;

CREATE TABLE `OrderDetailInfo` (
  `OId` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) DEFAULT NULL,
  `DishId` int(11) DEFAULT NULL,
  `Count` int(11) DEFAULT NULL,
  PRIMARY KEY (`OId`),
  KEY `OrderId` (`OrderId`),
  KEY `DishId` (`DishId`),
  CONSTRAINT `OrderDetailInfo_ibfk_1` FOREIGN KEY (`OrderId`) REFERENCES `OrderInfo` (`OId`),
  CONSTRAINT `OrderDetailInfo_ibfk_2` FOREIGN KEY (`DishId`) REFERENCES `DishInfo` (`DId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

/*Data for the table `OrderDetailInfo` */

insert  into `OrderDetailInfo`(`OId`,`OrderId`,`DishId`,`Count`) values (3,6,1,2),(4,7,2,10),(5,7,1,10),(6,8,2,6),(7,8,1,2),(8,9,2,4);

/*Table structure for table `OrderInfo` */

DROP TABLE IF EXISTS `OrderInfo`;

CREATE TABLE `OrderInfo` (
  `OId` int(11) NOT NULL AUTO_INCREMENT,
  `MemberId` int(11) DEFAULT NULL,
  `ODate` datetime DEFAULT NULL,
  `OMoney` decimal(7,2) DEFAULT NULL,
  `IsPay` tinyint(1) DEFAULT NULL,
  `TableId` int(11) DEFAULT NULL,
  `Discount` decimal(3,2) DEFAULT NULL,
  PRIMARY KEY (`OId`),
  KEY `MemberId` (`MemberId`),
  KEY `TableId` (`TableId`),
  CONSTRAINT `OrderInfo_ibfk_1` FOREIGN KEY (`MemberId`) REFERENCES `MemberInfo` (`MId`),
  CONSTRAINT `OrderInfo_ibfk_2` FOREIGN KEY (`TableId`) REFERENCES `TableInfo` (`TId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `OrderInfo` */

insert  into `OrderInfo`(`OId`,`MemberId`,`ODate`,`OMoney`,`IsPay`,`TableId`,`Discount`) values (6,2,'2015-03-29 13:03:44','60.00',1,1,'0.80'),(7,NULL,'2015-03-29 14:49:44','70.00',0,2,NULL),(8,NULL,'2015-03-29 22:29:17','180.00',0,1,NULL),(9,1,'2015-03-30 09:18:05','80.00',1,3,'1.00');

/*Table structure for table `TableInfo` */

DROP TABLE IF EXISTS `TableInfo`;

CREATE TABLE `TableInfo` (
  `TId` int(11) NOT NULL AUTO_INCREMENT,
  `TTitle` varchar(10) DEFAULT NULL,
  `THallId` int(11) DEFAULT NULL,
  `TIsFree` tinyint(1) DEFAULT NULL,
  `TIsDelete` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`TId`),
  KEY `THallId` (`THallId`),
  CONSTRAINT `TableInfo_ibfk_1` FOREIGN KEY (`THallId`) REFERENCES `HallInfo` (`HId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

/*Data for the table `TableInfo` */

insert  into `TableInfo`(`TId`,`TTitle`,`THallId`,`TIsFree`,`TIsDelete`) values (1,'t01',1,0,0),(2,'t02',1,0,0),(3,'t01',2,1,0),(4,'t02',2,1,0),(5,'北京',3,1,0),(6,'上海',3,1,0),(7,'西安',3,1,0);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
