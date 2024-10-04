CREATE TABLE public."groups" (
	id uuid NOT NULL,
	"name" varchar NOT NULL,
	uniquename varchar NOT NULL,
	createdat timestamptz NOT NULL,
	updatedat timestamptz NULL,
	CONSTRAINT groups_pkey PRIMARY KEY (id),
	CONSTRAINT groups_uniquename_key UNIQUE (uniquename)
);