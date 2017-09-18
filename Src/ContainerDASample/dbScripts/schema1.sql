-- Database: "ASPNETCORE"

-- DROP DATABASE "ASPNETCORE";

CREATE DATABASE "ASPNETCORE"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       LC_COLLATE = 'English_United States.1252'
       LC_CTYPE = 'English_United States.1252'
       CONNECTION LIMIT = -1;

	   -- Schema: container

-- DROP SCHEMA container;

CREATE SCHEMA container
  AUTHORIZATION postgres;

GRANT ALL ON SCHEMA container TO postgres;
GRANT ALL ON SCHEMA container TO public;
COMMENT ON SCHEMA container
  IS 'Testing container data access';

  -- Sequence: container."Employees_Id_seq"

-- DROP SEQUENCE container."Employees_Id_seq";

CREATE SEQUENCE container."Employees_Id_seq"
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 6
  CACHE 1;
ALTER TABLE container."Employees_Id_seq"
  OWNER TO postgres;

  -- Table: container."Employees"

-- DROP TABLE container."Employees";

CREATE TABLE container."Employees"
(
  "Id" integer NOT NULL DEFAULT nextval('container."Employees_Id_seq"'::regclass),
  "Name" text,
  CONSTRAINT "PK_Employees" PRIMARY KEY ("Id")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE container."Employees"
  OWNER TO postgres;
