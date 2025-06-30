/// <reference path="newgame.jsx" />
import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import useGames from "../hooks/useGames";
import '../assets/css/style.css';

function Home() {
    const changeLanguage = (lang) => {
        localStorage.setItem("language", lang);
        window.location.reload(); 
    };

    const { games, loading } = useGames();

    // Función para agregar al carrito
    const handleAddToCart = async (idGame) => {
        const token = localStorage.getItem("token");
        try {
            const response = await fetch("/cart/add", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept-Language": localStorage.getItem("language") || "es",
                    ...(token && { "Authorization": `Bearer ${token}` })
                },
                body: JSON.stringify({ idGame, amount: 1 })
            });

            if (response.ok) {
                alert("Juego agregado al carrito");
            } else {
                alert("Error al agregar al carrito");
            }
        } catch (error) {
            alert("Error de red al agregar al carrito");
        }
    };

    useEffect(() => {
        console.log("Juegos cargados:", games);
        const token = localStorage.getItem("token");
        console.log("Token actual:", token);
    }, [games]);
    return (
        <div>
            {/* Slider principal */}
            <div id="top-header">
                <div className="container">
                    <ul className="header-links pull-right">
                        <li>
                            <a href="https://g.co/kgs/LZZZ8K4">
                                <i className="fa fa-dollar"></i> USD
                            </a>
                        </li>
                        <li>
                            <a href="/Login">
                                <i className="fa fa-user-o"></i> Iniciar Sesión
                            </a>
                        </li>

                        <li>
                            <a href="/library"><i class="fa fa-book">

                            </i> Mi Biblioteca</a>
                        </li>

                        <li>
                            <Link to="/NewGame"><i className="fa fa-plus"></i> Agregar Juego</Link>
                        </li>


                        <li>
                            <a href="/categories/CreateCategory"><i class="fa fa-plus"></i> Agregar Categoria</a>
                        </li>

                        <li>
                            <a href="/InicioSecion/listUser"><i class="fa fa-user-o"></i>Accounts</a>
                        </li>

                        <li className="dropdown">
                            <a href="#" className="dropdown-toggle" data-toggle="dropdown">
                                <i className="fa fa-globe"></i> Idioma <b className="caret"></b>
                            </a>
                            <ul className="dropdown-menu">
                                <li><button className="dropdown-item" onClick={() => changeLanguage('es')}>Español</button></li>
                                <li><button className="dropdown-item" onClick={() => changeLanguage('en')}>English</button></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>

            <br /><br />

            <div id="header">
                <div className="container">
                    <div className="row">
                        <div className="col-md-3">
                            <div className="header-logo">
                            </div>
                        </div>

                        <div className="col-md-6">
                            <div className="header-search">
                                <form action="/Game/search" method="get">
                                    <select name="idCategory" className="input-select">
                                        <option value="">All Categories</option>
                                    </select>
                                    <input
                                        className="input"
                                        name="name"
                                        placeholder="Search here"
                                    />
                                    <input type="hidden" name="exact" value="false" />
                                    <button className="search-btn">Search</button>
                                </form>
                            </div>
                        </div>

                        <div className="col-md-3 clearfix">
                            <div className="header-ctn">
                                <div className="dropdown">
                                    <a className="dropdown-toggle" href="/wishlist/1">
                                        <i className="fa fa-heart-o"></i>
                                        <span>Your Wishlist</span>
                                        <div className="qty">0</div>
                                    </a>
                                </div>

                                <div className="dropdown">
                                    <a className="dropdown-toggle" href="/cart">
                                        <i className="fa fa-shopping-cart"></i>
                                        <span>Your Cart</span>
                                        <div className="qty">0</div>
                                    </a>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            {/* Sección de juegos */}
            <div className="section">
                <div className="container">
                    <div className="row">
                        <div className="col-md-12">
                            <h3 className="title mb-4">Juegos disponibles</h3>
                        </div>

                        {loading ? (
                            <div className="col-12">
                                <p>Cargando juegos...</p>
                            </div>
                        ) : games.length === 0 ? (
                            <div className="col-12">
                                <p>No hay juegos disponibles.</p>
                            </div>
                        ) : (
                            games.map((game) => (
                                <div className="col-md-3 col-sm-6 mb-4" key={game.idGame}>
                                    <div className="product">
                                        <div className="product-img">
                                            <img
                                                src={`/assets${game.image}`}
                                                alt={game.name}
                                                onError={(e) => {
                                                    e.target.onerror = null;
                                                    e.target.src = "/assets/img/placeholder.jpg"; // imagen por defecto si falla
                                                }}
                                            />
                                            <div className="product-label">
                                                <span className="new">NEW</span>
                                            </div>
                                        </div>
                                        <div className="product-body">
                                            <p className="product-category">
                                                {game.idCategories?.[0]?.name || "Sin categoría"}
                                            </p>
                                            <h3 className="product-name">
                                                <a href="#">{game.name}</a>
                                            </h3>
                                            <h4 className="product-price">₡{game.priceBuy}</h4>
                                            <div className="product-btns">
                                                <button className="add-to-wishlist" data-id={game.idGame}>
                                                    <i className="fa fa-heart-o"></i>
                                                    <span className="tooltipp">Agregar a favoritos</span>
                                                </button>
                                                <button className="quick-view" data-id={game.idGame}>
                                                    <i className="fa fa-eye"></i>
                                                    <span className="tooltipp">Ver más</span>
                                                </button>
                                            </div>
                                            {/* Botón ADD TO CART debajo de los botones de producto */}
                                            <div className="add-to-cart" style={{ marginTop: "10px" }}>
                                                <button
                                                    type="button"
                                                    className="add-to-cart-btn"
                                                    onClick={() => handleAddToCart(game.idGame)}
                                                >
                                                    <i className="fa fa-shopping-cart"></i> ADD TO CART
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            ))
                        )}
                    </div>
                </div>
            </div>

            {/* FOOTER */}
        </div>
    );
}

export default Home;
