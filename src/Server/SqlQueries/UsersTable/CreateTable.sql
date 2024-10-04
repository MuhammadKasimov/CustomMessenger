CREATE TABLE public.users (
	id uuid NOT NULL,
	username varchar NOT NULL,
	"password" varchar NOT NULL,
	"name" varchar NOT NULL,
	bio varchar NULL,
	phonenumber varchar NOT NULL,
	email varchar NULL,
	createdat timestamptz NOT NULL,
	updatedat timestamptz NULL,
	CONSTRAINT users_email_key UNIQUE (email),
	CONSTRAINT users_phonenumber_key UNIQUE (phonenumber),
	CONSTRAINT users_pkey PRIMARY KEY (id),
	CONSTRAINT users_username_key UNIQUE (username)
);