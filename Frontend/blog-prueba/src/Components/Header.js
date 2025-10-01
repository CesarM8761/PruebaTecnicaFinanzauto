import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../config";
import "../Styles/Header.css";
import logo from "../images/logo.png";

export default function Header({ isLoggedIn, onLogout }) {
    const [username, setUsername] = useState("");
    const [showCategorias, setShowCategorias] = useState(false);
    const [categorias, setCategorias] = useState([]);
    const idUsuarioLogeado = localStorage.getItem("idUsuarioLogeado");
    const navigate = useNavigate();

    useEffect(() => {
        if (idUsuarioLogeado) {
            fetch(`${API_BASE_URL}/Usuarios/${idUsuarioLogeado}`)
                .then((res) => res.json())
                .then((data) => setUsername(data.username))
                .catch((err) => console.error("Error fetching user data:", err));
        }
    }, [idUsuarioLogeado]);

    useEffect(() => {
        fetch(`${API_BASE_URL}/Categorias`)
            .then((res) => res.json())
            .then((data) => setCategorias(data))
            .catch((err) => console.error("Error fetching categories:", err));
    }, []);

    const handleLogoutClick = () => {
        onLogout();
        navigate("/");
    };

    return (
        <header className="header">
            <div className="header-left">
                <Link to="/">
                    <img src={logo} alt="Logo" className="logo" />
                </Link>
                <nav className="nav">
                    <Link to="/" className="nav-link">Inicio</Link>
                    <div className="dropdown">
                        <button
                            className="dropdown-button"
                            onClick={() => setShowCategorias(!showCategorias)}
                        >
                            Categorías
                        </button>
                        {showCategorias && (
                            <div className="dropdown-menu">
                                {categorias.map((cat) => (
                                    <Link
                                        key={cat.idCategoria}
                                        to={`/categorias/${cat.idCategoria}`}
                                        className="dropdown-item"
                                    >
                                        {cat.nombreCategoria}
                                    </Link>
                                ))}
                            </div>
                        )}
                    </div>
                </nav>
            </div>
            <div className="header-right">
                {!isLoggedIn ? (
                    <>
                        <Link to="/login" className="button-login">Iniciar sesión</Link>
                        <Link to="/registro" className="button-register">Registrarse</Link>
                    </>
                ) : (
                    <div className="user-actions">
                        <Link to={`/usuario/${idUsuarioLogeado}`} className="button-register">
                            {username}
                        </Link>
                        <button className="button-logout" onClick={handleLogoutClick}>
                            Cerrar sesión
                        </button>
                    </div>
                )}
            </div>
        </header>
    );
}
