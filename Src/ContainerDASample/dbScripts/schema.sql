CREATE SCHEMA container
  AUTHORIZATION postgres;
GRANT ALL ON SCHEMA container TO postgres;
GRANT ALL ON SCHEMA container TO public;
COMMENT ON SCHEMA container
  IS 'Testing container data access';
  CREATE SEQUENCE container."Employees_Id_seq"
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 6
  CACHE 1;
ALTER TABLE container."Employees_Id_seq"
  OWNER TO postgres;
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