import React from 'react';
import { useUserLibrary } from '../hooks/useUserLibrary';
import './LibraryView.css'; // tu CSS personalizado
import { useNavigate } from 'react-router-dom';

function LibraryView() {
    const { purchasedGames, rentedGames, loading } = useUserLibrary();
    const navigate = useNavigate();

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        navigate('/login');
    };

    const goHome = () => {
        navigate('/home');
    };

    if (loading) return <p className="text-center text-light">Cargando tu biblioteca...</p>;

    return (
        <div className="library-container bg-dark text-light min-vh-100">
            {/* NAVBAR */}
          {/* Encabezado similar al de Home */}
<div id="top-header" className="bg-danger text-light py-2">
    <div className="container d-flex justify-content-between align-items-center">
        <div className="fw-bold fs-5">🎮 GameVerse</div>
        <ul className="list-inline mb-0 d-flex gap-3 align-items-center">
            <li className="list-inline-item">
                <button className="btn btn-sm btn-light" onClick={goHome}>
                    🏠 Inicio
                </button>
            </li>
            <li className="list-inline-item">
                <button className="btn btn-sm btn-dark" onClick={handleLogout}>
                    🔓 Cerrar sesión
                </button>
            </li>
        </ul>
    </div>
</div>


            {/* CONTENIDO */}
            <div className="container py-5">
                <h2 className="mb-4"><i className="fa fa-gamepad me-2"></i> Juegos comprados</h2>
                <div className="row">
                    {purchasedGames.length === 0 ? (
                        <p>No has comprado juegos aún.</p>
                    ) : (
                        purchasedGames.map(game => (
                            <div className="col-md-4 mb-5" key={game.idGame}>
                                <div className="card h-100 bg-dark text-light shadow">
                                    <img
                                        src={`/assets${game.image}`}
                                        className="card-img-top"
                                        alt={game.title}
                                        onError={(e) => {
                                            e.target.onerror = null;
                                            e.target.src = "/assets/img/placeholder.jpg";
                                        }}
                                    />
                                    <div className="card-body">
                                        <h5 className="card-title">{game.title}</h5>
                                        <p className="card-text">{game.description}</p>
                                        <a href={game.url} className="btn btn-sm btn-outline-success" target="_blank" rel="noopener noreferrer">
                                            Jugar ahora
                                        </a>
                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>

                <h2 className="mt-5 mb-4"><i className="fa fa-clock-o me-2"></i> Juegos rentados</h2>
                <div className="row">
                    {rentedGames.length === 0 ? (
                        <p>No tienes juegos rentados actualmente.</p>
                    ) : (
                        rentedGames.map(game => (
                            <div className="col-md-4 mb-5" key={game.idGame}>
                                <div className="card h-100 bg-dark text-light shadow">
                                    <img
                                        src={`/assets${game.image}`}
                                        className="card-img-top"
                                        alt={game.title}
                                        onError={(e) => {
                                            e.target.onerror = null;
                                            e.target.src = "/assets/img/placeholder.jpg";
                                        }}
                                    />
                                    <div className="card-body">
                                        <h5 className="card-title">{game.title}</h5>
                                        <p className="card-text">{game.description}</p>
                                        <p className="text-warning">
                                            Disponible hasta: {new Date(game.expireDate).toLocaleDateString()}
                                        </p>
                                        <a
                                            href={game.url}
                                            className="btn btn-sm btn-outline-success"
                                            target="_blank"
                                            rel="noopener noreferrer"
                                        >
                                            Jugar ahora
                                        </a>


                                    </div>
                                </div>
                            </div>
                        ))
                    )}
                </div>
            </div>
        </div>
    );
}

export default LibraryView;
