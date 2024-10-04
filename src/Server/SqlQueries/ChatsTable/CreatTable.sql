CREATE TABLE public.chats (
	id uuid NOT NULL,
	firstuserid uuid NOT NULL,
	seconduserid uuid NOT NULL,
	createdat timestamptz NOT NULL,
	updatedat timestamptz NULL,
	CONSTRAINT chats_pkey PRIMARY KEY (id),
	CONSTRAINT chats_firstuserid_fkey FOREIGN KEY (firstuserid) REFERENCES public.users(id),
	CONSTRAINT chats_seconduserid_fkey FOREIGN KEY (seconduserid) REFERENCES public.users(id)
);