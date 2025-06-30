import React, { useState, useEffect } from "react";
import createGame from "../hooks/useNewGame";

function GameForm() {
    const [formData, setFormData] = useState({
        id: "",
        name: "",
        title: "",
        image: "",
        priceBuy: "",
        priceRent: "",
        url: "",
        description: "",
        categories: []
    });

    const handleChange = (e) => {
        const { name, value, type, selectedOptions } = e.target;
        if (type === "select-multiple") {
            const values = Array.from(selectedOptions, (option) => parseInt(option.value));
            setFormData({ ...formData, [name]: values });
        } else {
            setFormData({ ...formData, [name]: value });
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await createGame(formData);
            alert("Juego creado con éxito");
        } catch (error) {
            alert("Error al guardar el juego");
        }
    };

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
        <div className="login-container">
            <h2>Agregar Nuevo Juego</h2>
            <form onSubmit={handleSubmit}>

                <div className="form-group">
                    <label htmlFor="id">ID:</label>
                    <input type="number" id="id" name="id" value={formData.id} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="name">Nombre:</label>
                    <input type="text" id="name" name="name" value={formData.name} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="title">Título:</label>
                    <input type="text" id="title" name="title" value={formData.title} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="image">Imagen (URL):</label>
                    <input type="text" id="image" name="image" value={formData.image} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="priceBuy">Precio de compra:</label>
                    <input type="number" id="priceBuy" name="priceBuy" value={formData.priceBuy} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="priceRent">Precio de renta:</label>
                    <input type="number" id="priceRent" name="priceRent" value={formData.priceRent} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="url">URL del juego:</label>
                    <input type="text" id="url" name="url" value={formData.url} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="description">Descripción:</label>
                    <textarea id="description" name="description" value={formData.description} onChange={handleChange} required />
                </div>

                <div className="form-group">
                    <label htmlFor="categories">Categorías:</label>
                    <select name="categories" multiple value={formData.categories} onChange={handleChange} required>
                        <option value="1">Acción</option>
                        <option value="2">Aventura</option>
                    </select>
                </div>

                <button type="submit" className="btn">Guardar</button>
            </form>
        </div>
    );
}

export default GameForm;
