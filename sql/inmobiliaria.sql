CREATE DATABASE 'inmo_aavaldez';

CREATE TABLE `propietarios` (
  	`id` 			INT NOT NULL AUTO_INCREMENT,
	`nombre`  		VARCHAR(255) DEFAULT NULL,
	`apellido` 		VARCHAR(255) DEFAULT NULL,
	`dni`  			VARCHAR(16) DEFAULT NULL,
	`telefono` 		VARCHAR(160) DEFAULT NULL,
	`email`  		VARCHAR(160) DEFAULT NULL,
  	PRIMARY KEY(`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

CREATE TABLE `inquilinos` (
  	`id` 			INT NOT NULL AUTO_INCREMENT,
	`nombre`  		VARCHAR(255) DEFAULT NULL,
	`apellido` 		VARCHAR(255) DEFAULT NULL,
	`dni`  			VARCHAR(16) DEFAULT NULL,
	`telefono` 		VARCHAR(160) DEFAULT NULL,
	`email`  		VARCHAR(160) DEFAULT NULL,
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