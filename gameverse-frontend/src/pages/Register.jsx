import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import '../assets/css/bootstrap.min.css';
import '../assets/css/font-awesome.min.css';
import '../assets/css/style.css';

const Register = () => {
    const [form, setForm] = useState({
        username: '',
        email: '',
        password: '',
        rol: 'USER'
    });

    const handleChange = (e) => {
        setForm({ ...form, [e.target.id]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch("https://localhost:7214/api/auth/register", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Accept-Language": localStorage.getItem("language") || "es"
                },
                body: JSON.stringify(form),
            });

            const contentType = response.headers.get("content-type");

            if (!response.ok) {
                const errorMessage = contentType && contentType.includes("application/json")
                    ? (await response.json()).message
                    : await response.text();

                console.error("Error del servidor:", errorMessage);
                alert("Error al registrar: " + errorMessage);
                return;
            }

            
            if (contentType && contentType.includes("application/json")) {
                const data = await response.json();
                console.log("Registro exitoso (JSON):", data);
            } else {
                const text = await response.text();
                console.log("Registro exitoso (texto):", text);
            }

            alert("¡Registro exitoso!");
            window.location.href = "/login";

        } catch (error) {
            console.error("Error de red al registrar:", error);
            alert("Error de red al intentar registrarte.");
        }
    };


    return (
        <div>
            <header>
                <div id="top-header">
                    <div className="container">
                        <ul className="header-links pull-left">
                            <li><i className="fa fa-phone"></i> +506 0808-0808</li>
                            <li><i className="fa fa-envelope-o"></i> GameVerse@gmail.com</li>
                        </ul>
                        <ul className="header-links pull-right">
                            <li><i className="fa fa-dollar"></i> USD</li>
                            <li><i className="fa fa-user-o"></i> My Account</li>
                        </ul>
                    </div>
                </div>

                <div id="header">
                    <div className="container">
                        <div className="row">
                            <div className="col-md-3">
                                <div className="header-logo">
                                    <Link to="/" className="logo">
                                        <img src="/assets/img/logo.png" alt="Logo GameVerse" />
                                    </Link>
                                </div>
                            </div>

                            <div className="login-container">
                                <h2>Regístrate</h2>
                                <form onSubmit={handleSubmit}>
                                    <div className="form-group">
                                        <label htmlFor="username">Nombre de Usuario</label>
                                        <input
                                            type="text"
                                            id="username"
                                            required
                                            value={form.username}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="email">Correo Electrónico</label>
                                        <input
                                            type="email"
                                            id="email"
                                            required
                                            value={form.email}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="password">Contraseña</label>
                                        <input
                                            type="password"
                                            id="password"
                                            required
                                            value={form.password}
                                            onChange={handleChange}
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="rol">Rol:</label>
                                        <select
                                            id="rol"
                                            className="form-control"
                                            value={form.rol}
                                            onChange={handleChange}
                                        >
                                            <option value="USER">Usuario</option>
                                            <option value="ADMIN">Administrador</option>
                                        </select>
                                    </div>
                                    <button type="submit" className="btn">Registrar</button>
                                </form>

                                <div className="login-link">
                                    ¿Ya tienes cuenta? <Link to="/login">Inicia Sesión</Link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </header>
        </div>
    );
};

export default Register;
