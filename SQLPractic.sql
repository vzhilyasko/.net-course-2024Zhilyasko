create table employee
(
	id uuid not null
		constraint employee_pk
			primary key,
	first_name varchar(100) not null,
	last_name varchar(100) not null,
	middle_name varchar(100),
	birthday date not null,
	e_mail varchar(60) not null,
	phone_number varchar (20) not null,
	passport_seriya varchar(4) not null,
	passport_number varchar(7) not null,
	depatment varchar(150) not null,
	job_title varchar(150) not null,
	salary decimal not null
);

create unique index employee_e_mail_uindex
	on employee (e_mail);
create unique index employee_phone_number_uindex
	on employee (phone_number);

create table client
(
	id uuid not null
		constraint client_pk
			primary key,
	first_name varchar(100) not null,
	last_name varchar(100) not null,
	middle_name varchar(100),
	birthday date not null,
	e_mail varchar(60) not null,
	phone_number varchar (20) not null,
	passport_seriya varchar(4) not null,
	passport_number varchar(7) not null
);

create unique index client_e_mail_uindex
	on client (e_mail);
create unique index client_phone_number_uindex
	on client (phone_number);

create table account
(
	id uuid not null
		constraint account_pk
			primary key,
	currency_name varchar(3) not null,
	amount decimal,
	client_id uuid not null
);

alter table account
	add constraint account_client_id_fk
		foreign key (client_id) references client;


insert
into employee (id, first_name, last_name, middle_name, birthday, e_mail, phone_number, passport_seriya, passport_number, depatment, job_title, salary)
values  (gen_random_uuid(),'Иванов', 'Иван', 'Иванович', '21.02.1985', 'ivanov.i@rambler.ru', '00-(373)-778-56-654', '1-ПР', '2152448', 'Разработки','Программмист',12145.25),
        (gen_random_uuid(),'Петров', 'Петр', 'Петрович', '21.02.1995', 'зуекщм.i@gmail.ru', '00-(373)-778-56-7845', '1-ПР', '2547448', 'Разработки','Программмист', 25487.78),
        (gen_random_uuid(),'Сидоров', 'Сидор', 'Иванович', '21.02.2001', 'ssid.i@ya.ru', '00-(373)-779-20-654', '1-ПР', '0021547', 'Маркетинг','Консультант', 5487.25),
        (gen_random_uuid(),'Козлевич', 'Адам', 'Каземирович', '12.01.1975', 'Kozlik.i@ya.ru', '00-(373)-775-56-254', '1-ПР', '0024187', 'Администрация','Заместитель директора', 21485.25);


insert
into client (id, first_name, last_name, middle_name, birthday, e_mail, phone_number, passport_seriya, passport_number)
values (gen_random_uuid(),'Иванов', 'Иван', 'Иванович', '21.02.1985', 'ivanov.i@rambler.ru', '00-(373)-778-56-654', '1-ПР', '2152448'),
       (gen_random_uuid(),'Петров', 'Петр', 'Петрович', '21.02.2001', 'зуекщм.i@gmail.ru', '00-(373)-778-56-7845', '1-ПР', '2547448'),
       (gen_random_uuid(),'Сидоров', 'Сидор', 'Иванович', '21.02.2001', 'ssid.i@ya.ru', '00-(373)-779-20-654', '1-ПР', '0021547'),
       (gen_random_uuid(),'Козлевич', 'Адам', 'Каземирович', '12.01.1975', 'Kozlik.i@ya.ru', '00-(373)-775-56-254', '1-ПР', '0024187');


insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'USD', '21547.12', (select id from client where e_mail = 'ivanov.i@rambler.ru'));

insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'UAH', '215478', (select id from client where e_mail = 'ivanov.i@rambler.ru'));

insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'RUB', '254785', (select id from client where e_mail = 'ivanov.i@rambler.ru'));


insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'USD', '654878.21', (select id from client where e_mail = 'зуекщм.i@gmail.ru'));

insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'UAH', '897854.87', (select id from client where e_mail = 'зуекщм.i@gmail.ru'));

insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'RUB', '1245787.32', (select id from client where e_mail = 'зуекщм.i@gmail.ru'));


insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'USD', '100000000.00', (select id from client where e_mail = 'ssid.i@ya.ru'));


insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'UAH', '16487965.54', (select id from client where e_mail = 'ssid.i@ya.ru'));


insert
into account(id, currency_name, amount, client_id)
values (gen_random_uuid(), 'RUB', '548979854.25', (select id from client where e_mail = 'Kozlik.i@ya.ru'));


select cl.first_name,
       cl.last_name,
       cl.middle_name,
       ac.currency_name,
       ac.amount
from account ac
left join client cl on ac.client_id = cl.id
where currency_name = 'USD'
and amount <= 700000
order by  amount;

select cl.first_name,
       cl.last_name,
       cl.middle_name,
       ac.currency_name,
       ac.amount
from account ac
left join client cl on ac.client_id = cl.id
where currency_name = 'USD'
and ac.amount =  (select min(amount) from account where ac.currency_name ='USD');

select sum(ac.amount)
from account ac
where currency_name = 'USD';

select cl.first_name,
       cl.last_name,
       cl.middle_name,
       ac.currency_name,
       ac.amount
from account ac
left join client cl on ac.client_id = cl.id
ORDER BY
cl.first_name, ac.currency_name;

select cl.first_name,
       cl.last_name,
       cl.middle_name,
       date_part('year', CURRENT_DATE) - date_part('year', cl.birthday)
from client cl
ORDER BY
date_part('year', CURRENT_DATE) - date_part('year', cl.birthday) desc ;

select
       date_part('year', CURRENT_DATE) - date_part('year', cl.birthday),
       count(*)
from client cl
GROUP BY
date_part('year', cl.birthday);

select
        date_part('year', cl.birthday),
        count(*)
from client cl
GROUP BY
date_part('year', cl.birthday);

select *
from client cl
LIMIT 2;