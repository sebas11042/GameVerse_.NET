import React from "react";

function Home() {
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
                                {/* Repetí más banners si tu plantilla los tenía */}
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

            {/* Agregá más secciones aquí (productos, footer, etc.) */}
        </div>
    );
}

export default Home;
