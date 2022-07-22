CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718113928_InitialCreatePostgresql') THEN
    CREATE TABLE restaurants (
        "Id" uuid NOT NULL,
        name text NOT NULL,
        address text NOT NULL,
        "isDeliveryFree" boolean NOT NULL,
        CONSTRAINT "PK_restaurants" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718113928_InitialCreatePostgresql') THEN
    CREATE TABLE users (
        "Id" uuid NOT NULL,
        username text NOT NULL,
        email text NOT NULL,
        password text NOT NULL,
        address text NOT NULL,
        credit double precision NOT NULL,
        CONSTRAINT "PK_users" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718113928_InitialCreatePostgresql') THEN
    CREATE TABLE foods (
        "Id" uuid NOT NULL,
        name text NOT NULL,
        price real NOT NULL,
        "restaurantId" uuid NULL,
        CONSTRAINT "PK_foods" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_foods_restaurants_restaurantId" FOREIGN KEY ("restaurantId") REFERENCES restaurants ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718113928_InitialCreatePostgresql') THEN
    CREATE INDEX "IX_foods_restaurantId" ON foods ("restaurantId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718113928_InitialCreatePostgresql') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20220718113928_InitialCreatePostgresql', '6.0.5');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718142158_OrderCreate') THEN
    CREATE TABLE orders (
        "orderId" uuid NOT NULL,
        "priceSum" double precision NOT NULL,
        "deliveryFee" double precision NOT NULL,
        price double precision NOT NULL,
        "createdOn" timestamp with time zone NOT NULL,
        foods text[] NOT NULL,
        "isCompleted" boolean NOT NULL,
        CONSTRAINT "PK_orders" PRIMARY KEY ("orderId")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220718142158_OrderCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20220718142158_OrderCreate', '6.0.5');
    END IF;
END $EF$;
COMMIT;

