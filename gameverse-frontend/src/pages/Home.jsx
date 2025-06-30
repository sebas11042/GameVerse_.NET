import React, { useEffect } from "react";
import useGames from "../hooks/UseGames";

function Home() {
    const { games, loading } = useGames();

    useEffect(() => {
        console.log("Juegos cargados:", games);
        const token = localStorage.getItem("token");
        console.log("Token actual:", token);
    }, [games]);

    return (
        <div>
            {/* Slider principal */}
            <div id="carousel" className="section">
                <div className="container">
                    <div className="row">
                        <div className="col-md-12">
                            <div id="home-slick">
                                <div className="banner banner-1">
                                    <img src="/assets/img/banner01.jpg" alt="Banner 1" />
                                    <div className="banner-caption text-center">
                                        <h1 className="primary-color">BIENVENIDO A GAMEVERSE</h1>
                                        <button className="btn btn-warning">Explorar juegos</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {/* Sección destacada */}
            <div className="section">
                <div className="container">
                    <div className="row">
                        <div className="col-md-4">
                            <div className="feature">
                                <i className="fa fa-rocket"></i>
                                <h3>Lanzamientos</h3>
                                <p>Los juegos más nuevos del mercado disponibles para vos.</p>
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="feature">
                                <i className="fa fa-tags"></i>
                                <h3>Descuentos</h3>
                                <p>Promociones exclusivas para miembros registrados.</p>
                            </div>
                        </div>
                        <div className="col-md-4">
                            <div className="feature">
                                <i className="fa fa-star"></i>
                                <h3>Recomendados</h3>
                                <p>Juegos destacados seleccionados especialmente para vos.</p>
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
                                        </div>
                                    </div>
                                </div>
                            ))
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Home;
