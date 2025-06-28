

IF DB_ID('GameVerseDB') IS NOT NULL
BEGIN
    ALTER DATABASE GameVerseDB SET MULTI_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GameVerseDB;
END


CREATE DATABASE GameVerseDB;
GO

USE GameVerseDB;
GO

-- Tabla: Users
DROP TABLE IF EXISTS Wishlist;
DROP TABLE IF EXISTS Shopping_Cart;
DROP TABLE IF EXISTS Rental_Detail;
DROP TABLE IF EXISTS Rentals;
DROP TABLE IF EXISTS Purchase_Detail;
DROP TABLE IF EXISTS Shopping;
DROP TABLE IF EXISTS Games_Category;
DROP TABLE IF EXISTS Games;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Users;  -- Ahora sí se puede borrar

CREATE TABLE Users (
    id_user INT IDENTITY(1,1) PRIMARY KEY, -- ✅ Autoincremental
    email NVARCHAR(255),
    password NVARCHAR(255),
    rol NVARCHAR(255),
    username NVARCHAR(255)
);


-- Tabla: Categories
CREATE TABLE Categories (
    id_category INT PRIMARY KEY,
    name NVARCHAR(255)
);

-- Tabla: Games
CREATE TABLE Games (
    id_game INT PRIMARY KEY,
    description NVARCHAR(255),
    image NVARCHAR(255),
    name NVARCHAR(255),
    price_buy INT,
    title NVARCHAR(255),
    url NVARCHAR(255),
    price_rental INT
);

-- Tabla: Games_Category
CREATE TABLE Games_Category (
    id_game INT,
    id_category INT,
    CONSTRAINT PK_GamesCategory PRIMARY KEY (id_game, id_category),
    CONSTRAINT FK_GC_Game FOREIGN KEY (id_game) REFERENCES Games(id_game),
    CONSTRAINT FK_GC_Category FOREIGN KEY (id_category) REFERENCES Categories(id_category)
);

-- Tabla: Shopping
CREATE TABLE Shopping (
    id_buy INT PRIMARY KEY,
    buy_date DATE,
    total INT,
    id_user INT,
    CONSTRAINT FK_Shopping_User FOREIGN KEY (id_user) REFERENCES Users(id_user)
);

-- Tabla: Purchase_Detail
CREATE TABLE Purchase_Detail (
    id_buy INT,
    id_game INT,
    amount INT,
    price INT,
    CONSTRAINT PK_Purchase PRIMARY KEY (id_buy, id_game),
    CONSTRAINT FK_PD_Buy FOREIGN KEY (id_buy) REFERENCES Shopping(id_buy),
    CONSTRAINT FK_PD_Game FOREIGN KEY (id_game) REFERENCES Games(id_game)
);

-- Tabla: Rentals
CREATE TABLE Rentals (
    id_rent INT PRIMARY KEY,
    start_date DATE,
    end_date DATE,
    rent_days INT,
    total INT,
    id_user INT,
    status BIT DEFAULT 1,
    CONSTRAINT FK_Rentals_User FOREIGN KEY (id_user) REFERENCES Users(id_user)
);

-- Tabla: Rental_Detail
CREATE TABLE Rental_Detail (
    id_rental INT,
    id_game INT,
    amount INT,
    rental_date DATE,
    expire_date DATE,
    price INT,
    CONSTRAINT PK_RentalDetail PRIMARY KEY (id_rental, id_game),
    CONSTRAINT FK_RD_Rental FOREIGN KEY (id_rental) REFERENCES Rentals(id_rent),
    CONSTRAINT FK_RD_Game FOREIGN KEY (id_game) REFERENCES Games(id_game)
);

-- Tabla: Shopping_Cart
CREATE TABLE Shopping_Cart (
    id_user INT,
    id_game INT,
    amount INT,
    price INT,
    CONSTRAINT PK_ShoppingCart PRIMARY KEY (id_user, id_game),
    CONSTRAINT FK_SC_User FOREIGN KEY (id_user) REFERENCES Users(id_user),
    CONSTRAINT FK_SC_Game FOREIGN KEY (id_game) REFERENCES Games(id_game)
);

-- Tabla: Wishlist
CREATE TABLE Wishlist (
    id_user INT,
    id_game INT,
    added_at DATE,
    CONSTRAINT PK_Wishlist PRIMARY KEY (id_user, id_game),
    CONSTRAINT FK_WL_User FOREIGN KEY (id_user) REFERENCES Users(id_user),
    CONSTRAINT FK_WL_Game FOREIGN KEY (id_game) REFERENCES Games(id_game)
);



-- INSERSIONES


INSERT INTO Categories (id_category, name) VALUES
(1, 'Plataforma'),
(2, 'Arcade'),
(3, 'Puzzle');

INSERT INTO Games (id_game, description, image, name, price_buy, title, url, price_rental) VALUES
(1, 'Clásico juego de Mario Bros', '/img/mario.jpg', 'Mario Infinite', 15, 'Mario', '/games/mario/index.html', 5),
(2, 'Juego retro de Pacman', '/img/pacman.jpg', 'Pacman', 10, 'Pacman', '/games/pacman/index.html', 4);

INSERT INTO Games_Category (id_game, id_category) VALUES
(1, 1),
(2, 2);

SELECT * FROM Games;

INSERT INTO Users (email, password, rol, username) VALUES
('admin@gameverse.com', 'admin123', 'admin', 'admin'),
('user1@gameverse.com', 'user123', 'user', 'user1');


DELETE FROM Users;
SELECT * FROM Users;

