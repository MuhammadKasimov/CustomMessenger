CREATE TABLE groups(
	id uuid NOT NULL,
	name varchar NOT NULL,
	uniquename varchar unique NOT NULL,
	createdat timestamp NOT NULL,
	updatedat timestamp
);