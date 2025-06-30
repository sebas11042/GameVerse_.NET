import React, { useEffect } from "react";


function GameForm() {
    useEffect(() => {
        const scripts = [
            "/js/jquery.min.js",
            "/js/bootstrap.min.js",
            "/js/slick.min.js",
            "/js/nouislider.min.js",
            "/js/jquery.zoom.min.js",
            "/js/main.js",
        ];

        scripts.forEach((src) => {
            const script = document.createElement("script");
            script.src = src;
            script.async = true;
            document.body.appendChild(script);
        });
    }, []);

    return (
        <div>
            <header>
                <div id="top-header">
                    <div className="container">
                        <ul className="header-links pull-left">
                            <li><a href="#"><i className="fa fa-phone"></i> +506 0808-0808</a></li>
                            <li><a href="#"><i className="fa fa-envelope-o"></i> GameVerse@gmial.com</a></li>
                        </ul>
                        <ul className="header-links pull-right">
                            <li><a href="https://g.co/kgs/LZZZ8K4"><i className="fa fa-dollar"></i> USD</a></li>
                            <li><a href="/InicioSecion"><i className="fa fa-user-o"></i> My Account</a></li>
                            <li><a href="/"><i className="fa fa-user-o"></i> Juegos</a></li>
                        </ul>
                    </div>
                </div>

                <div id="header">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-3">
                                <div className="header-logo">
                                </div>
                            </div>

                            <div className="login-container">
                                <h2>Agregar Nuevo Juego</h2>
                                <form action="/Game/save" method="post">
                                    <div className="form-group">
                                        <label htmlFor="name">Nombre:</label>
                                        <input type="text" id="name" name="name" required />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="title">Título:</label>
                                        <input type="text" id="title" name="title" required />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="image">Imagen (URL):</label>
                                        <input type="text" id="image" name="image" required />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="priceBuy">Precio de compra:</label>
                                        <input type="number" id="priceBuy" name="priceBuy" required />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="url">URL del juego:</label>
                                        <input type="text" id="url" name="url" required />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="description">Descripción:</label>
                                        <textarea id="description" name="description" required></textarea>
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="categories">Categorías:</label>
                                        <select name="categories" multiple required>
                                            {/* Aquí deberías mapear las categorías dinámicamente */}
                                            <option value="1">Acción</option>
                                            <option value="2">Aventura</option>
                                        </select>
                                    </div>

                                    <button type="submit" className="btn">Guardar</button>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </header>

            <footer id="footer">
                <div className="section">
                    <div className="container">
                        <div className="info">
                            <div className="col-md-3 col-xs-6">
                                <div className="footer">
                                    <ul className="footer-links">
                                        <li><a href="#"><i className="fa fa-phone"></i>+506 0808-0808</a></li>
                                        <li><a href="#"><i className="fa fa-envelope-o"></i>GameVerse@email.com</a></li>
                                    </ul>
                                </div>
                            </div>
                            <div className="clearfix visible-xs"></div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    );
}

export default GameForm;
