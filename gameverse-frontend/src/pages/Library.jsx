import React, { useState } from 'react';

function Library() {
    // Simulación de datos, reemplaza por fetch o props según tu lógica real
    const [purchasedGames] = useState([
        // { name: "Juego 1", description: "Descripción del juego 1" },
    ]);
    const [rentedGames] = useState([
        // { name: "Juego 2", description: "Descripción del juego 2" },
    ]);

    return (
        <div className="container mt-4">
            <h2>Juegos Comprados</h2>
            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>Juego</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    {purchasedGames.length === 0 ? (
                        <tr>
                            <td colSpan="2">No has comprado ningún juego aún.</td>
                        </tr>
                    ) : (
                        purchasedGames.map((game, index) => (
                            <tr key={index}>
                                <td>{game.name}</td>
                                <td>{game.description}</td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>

            <h2>Juegos Alquilados</h2>
            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>Juego</th>
                        <th>Descripción</th>
                    </tr>
                </thead>
                <tbody>
                    {rentedGames.length === 0 ? (
                        <tr>
                            <td colSpan="2">No tienes juegos alquilados activos.</td>
                        </tr>
                    ) : (
                        rentedGames.map((game, index) => (
                            <tr key={index}>
                                <td>{game.name}</td>
                                <td>{game.description}</td>
                            </tr>
                        ))
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default Library;
