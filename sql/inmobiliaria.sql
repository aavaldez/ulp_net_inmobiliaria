CREATE DATABASE 'inmo_aavaldez';

CREATE TABLE `propietarios` (
  `id` INT NOT NULL AUTO_INCREMENT,
	`nombre` VARCHAR(255) NOT NULL,
	`apellido` VARCHAR(255) NOT  NULL,
	`dni` VARCHAR(16) NOT NULL,
	`telefono` VARCHAR(160) DEFAULT NULL,
	`email` VARCHAR(160) DEFAULT NULL,
  	PRIMARY KEY(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `inquilinos` (
  `id` INT NOT NULL AUTO_INCREMENT,
	`nombre` VARCHAR(255) NOT NULL,
	`apellido` VARCHAR(255) NOT NULL,
	`dni` VARCHAR(16) NOT NULL,
	`telefono` VARCHAR(160) DEFAULT NULL,
	`email` VARCHAR(160) DEFAULT NULL,
  	PRIMARY KEY(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `inmuebles` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`propietarioId` INT NOT NULL,
	`direccion` VARCHAR(255) DEFAULT NULL,
	`ambientes` VARCHAR(255) DEFAULT NULL,
	`superficie` INT NOT NULL DEFAULT 0,
	`latitud` DECIMAL(10,2) NOT NULL DEFAULT 0,
	`longitud` DECIMAL(10,2) NOT NULL DEFAULT 0,
  	PRIMARY KEY(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

ALTER TABLE `inmuebles` ADD CONSTRAINT `inmuebles_propietarioId` FOREIGN KEY (`propietarioId`) REFERENCES `propietarios`(`id`) ON DELETE CASCADE ON UPDATE CASCADE;

CREATE TABLE `contratos` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`inquilinoId` INT NOT NULL,
	`inmuebleId` INT NOT NULL,
	`desde` DATETIME DEFAULT NULL,
	`hasta` DATETIME DEFAULT NULL,
	`valor` DECIMAL(10,2) NOT NULL DEFAULT 0,
  	PRIMARY KEY(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


dotnet-aspnet-codegenerator view Index List -outDir "Views/Propietarios" -udl --model ulp_net_inmobiliaria.Models.Propietario -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Propietarios" -udl --model ulp_net_inmobiliaria.Models.Propietario -f
dotnet-aspnet-codegenerator view Edit Edit -outDir "Views/Propietarios" -udl --model ulp_net_inmobiliaria.Models.Propietario -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Propietarios" -udl --model ulp_net_inmobiliaria.Models.Propietario -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Propietarios" -udl --model ulp_net_inmobiliaria.Models.Propietario -f

dotnet-aspnet-codegenerator view Index List -outDir "Views/Inquilinos" -udl --model ulp_net_inmobiliaria.Models.Inquilino -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Inquilinos" -udl --model ulp_net_inmobiliaria.Models.Inquilino -f
dotnet-aspnet-codegenerator view Edit Edit -outDir "Views/Inquilinos" -udl --model ulp_net_inmobiliaria.Models.Inquilino -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Inquilinos" -udl --model ulp_net_inmobiliaria.Models.Inquilino -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Inquilinos" -udl --model ulp_net_inmobiliaria.Models.Inquilino -f

dotnet-aspnet-codegenerator view Index List -outDir "Views/Inmuebles" -udl --model ulp_net_inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Inmuebles" -udl --model ulp_net_inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Edit Edit -outDir "Views/Inmuebles" -udl --model ulp_net_inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Inmuebles" -udl --model ulp_net_inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Inmuebles" -udl --model ulp_net_inmobiliaria.Models.Inmueble -f