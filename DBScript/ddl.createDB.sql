CREATE KEYSPACE IF NOT EXISTS wine_keyspace
WITH REPLICATION = {'class': 'SimpleStrategy', 'replication_factor': 1}
AND durable_writes = true;

create table wine
(
    wine_id          int primary key,
    appellation      text,
    appellation_slug text,
    classification   text,
    color            text,
    confidence_index text,
    country          text,
    date             text,
    is_primeurs      boolean,
    journalist_count int,
    lwin             text,
    lwin11           text,
    region           text,
    score            double,
    vintage          text,
    wine             text,
    wine_slug        text,
    wine_type        text
)
with comment = 'Вина';

create table user
(
    userid   int primary key,
    email    text,
    name     text,
    password int
)
with comment = 'Пользователи';

-- Импортируй данные в БД из файла DBScript/newWines.csv
