import React from 'react';
import useGames from '../hooks/useGames';

function Home() {
    const { games, loading } = useGames();

    if (loading) {
        return <div>Cargando juegos...</div>;
    }

    return (
        <div className="section">
            <div className="container">
                <div className="row">
                    <div className="col-md-12">
                        <div className="section-title">
                            <h3 className="title">New Games</h3>
                            <div className="section-nav">
                                <ul className="section-tab-nav tab-nav">
                                    <li><a href="/Game/list">Ver más</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div className="col-md-12">
                        <div className="products-tabs">
                            <div id="tab1" className="tab-pane active">
                                <div className="products-slick" data-nav="#slick-nav-1">
                                    {games.map((game) => (
                                        <div className="product" key={game.idGame}>
                                            <div className="product-img">
                                                <img
                                                    src={`/assets/img/${game.image}`} // Asegúrate que la imagen exista ahí
                                                    alt={game.name}
                                                />
                                                <div className="product-label">
                                                    <span className="new">NEW</span>
                                                </div>
                                            </div>
                                            <div className="product-body">
                                                <p className="product-category">
                                                    {game.idCategories && game.idCategories.length > 0
                                                        ? game.idCategories[0].name
                                                        : 'Sin categoría'}
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
                                    ))}

                                </div>
                                <div id="slick-nav-1" className="products-slick-nav"></div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    );
}

export default Home;
