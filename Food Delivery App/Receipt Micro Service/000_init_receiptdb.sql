CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220721182514_InitialCreate') THEN
    CREATE TABLE receipts (
        "Id" uuid NOT NULL,
        "userEmail" text NOT NULL,
        "priceSum" double precision NOT NULL,
        "deliveryFee" double precision NOT NULL,
        price double precision NOT NULL,
        "createdOn" timestamp with time zone NOT NULL,
        foods text[] NOT NULL,
        "isCompleted" boolean NOT NULL,
        CONSTRAINT "PK_receipts" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20220721182514_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20220721182514_InitialCreate', '6.0.5');
    END IF;
END $EF$;
COMMIT;

