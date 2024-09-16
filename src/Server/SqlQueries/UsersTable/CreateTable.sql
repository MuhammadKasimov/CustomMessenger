CREATE TABLE users (
	id uuid NOT NULL,
	username varchar unique NOT NULL,
	password varchar NOT NULL,
	name varchar NOT NULL,
	bio varchar,
	phonenumber varchar unique NOT NULL,
	email varchar unique NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp,
	PRIMARY KEY (id)
);