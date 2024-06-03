create table "CarMake" (
	"Id" SERIAL primary key not null,
	"MakeName" text not null,
	"MakeCountry" text not null,
	"IsActive" boolean default true
);

create table "Car" (
	"Id" SERIAL primary key not null,
	"CarModel" text not null,
	"YearOfMake" int not null,
	"Mileage" int not null,
	"Horsepower" int not null,
	"IsActive" boolean default true,
	"CarMakeId" int,
	constraint "FK_Car_CarMake_CarMakeId"
		foreign key ("CarMakeId")
		references "CarMake"("Id")
		on delete set NULL
);
