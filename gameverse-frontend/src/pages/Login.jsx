import React, { useEffect, useState } from "react";


function LoginForm() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

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
    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("https://localhost:7214/api/auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept-Language": localStorage.getItem("language") || "es"
                },
                body: JSON.stringify({
                    email: email,
                    password: password
                }),
            });

            if (!response.ok) {
                if (response.status === 401) {
                    alert("Credenciales incorrectas.");
                } else {
                    alert("Error del servidor al iniciar sesión.");
                }
                return;
            }

            const data = await response.json();
            console.log("Inicio de sesión exitoso:", data);


            localStorage.setItem("token", data.token);
            localStorage.setItem("userId", data.idUser);
            localStorage.setItem("username", data.username);


            alert("Inicio de sesión exitoso.");
            window.location.href = "/home";

        } catch (error) {
            console.error("Error de red al iniciar sesión:", error);
            alert("Error de red al intentar iniciar sesión.");
        }
    };



    return (
        <div>
            <header>
                <div id="top-header">
                    <div className="container">
                        <ul className="header-links pull-left">
                            <li>
                                <a href="javascript:void(0)">
                                    <i className="fa fa-phone"></i> +506 0808-0808
                                </a>
                            </li>
                            <li>
                                <a href="javascript:void(0)">
                                    <i className="fa fa-envelope-o"></i> GameVerse@gmail.com
                                </a>
                            </li>
                        </ul>
                        <ul className="header-links pull-right">
                            <li>
                                <a href="https://g.co/kgs/LZZZ8K4">
                                    <i className="fa fa-dollar"></i> USD
                                </a>
                            </li>
                            <li>
                                <a href="/login">
                                    <i className="fa fa-user-o"></i> My Account
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>

                <div id="header">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-3">
                            </div>

                            <div className="login-container">
                                <h2>Iniciar Sesión</h2>
                                <form onSubmit={handleSubmit}>
                                    <div className="form-group">
                                        <label htmlFor="email">Correo Electrónico</label>
                                        <input
                                            type="email"
                                            id="email"
                                            name="email"
                                            value={email}
                                            onChange={(e) => setEmail(e.target.value)}
                                            required
                                        />
                                    </div>

                                    <div className="form-group">
                                        <label htmlFor="password">Contraseña</label>
                                        <input
                                            type="password"
                                            id="password"
                                            name="password"
                                            value={password}
                                            onChange={(e) => setPassword(e.target.value)}
                                            required
                                        />
                                    </div>

                                    <button type="submit" className="btn">Ingresar</button>
                                </form>

                                <div id="buttonDiv"></div>
                                <div className="register-link">
                                    ¿No tienes cuenta? <a href="/register">Regístrate</a>
                                </div>
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
                                        <li>
                                            <a href="javascript:void(0)">
                                                <i className="fa fa-phone"></i> +506 0808-0808
                                            </a>
                                        </li>
                                        <li>
                                            <a href="javascript:void(0)">
                                                <i className="fa fa-envelope-o"></i> GameVerse@email.com
                                            </a>
                                        </li>
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

export default LoginForm;
