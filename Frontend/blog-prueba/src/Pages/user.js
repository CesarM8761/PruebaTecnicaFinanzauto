import React, { useState, useEffect } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { API_BASE_URL } from "../config"

export default function User()
{
    const navigate = useNavigate()
    const { id } = useParams() // id del usuario consultado
    const loggedUserId = localStorage.getItem("idUsuarioLogeado")
    const tokenJWT = localStorage.getItem("TokenJWT")
    const [usuario, setUsuario] = useState(null)
    const [publicaciones, setPublicaciones] = useState([])

    useEffect(() =>
    {
        if (!id) return
        fetch(`${API_BASE_URL}/Usuarios/${id}`)
            .then(res =>
            {
                if (!res.ok) throw new Error("Usuario no encontrado")
                return res.json()
            })
            .then(data =>
            {
                if (!data || Object.keys(data).length === 0)
                {
                    navigate("/404")
                } else
                {
                    setUsuario(data)
                }
            })
            .catch(err =>
            {
                console.error(err)
                navigate("/404")
            })
        fetch(`${API_BASE_URL}/Publicaciones/Usuario/${id}`)
            .then(res => res.json())
            .then(data => setPublicaciones(data))
            .catch(err => console.error(err))
    }, [id, navigate])


    const handleDelete = (idPublicacion) =>
    {
        if (!window.confirm("¿Deseas eliminar esta publicación?")) return
        fetch(`${API_BASE_URL}/Publicaciones/${idPublicacion}`, {
            method: "DELETE",
            headers: { "Authorization": `Bearer ${tokenJWT}` }
        })
            .then(res =>
            {
                if (res.ok)
                {
                    setPublicaciones(prev => prev.filter(pub => pub.idPublicacion !== idPublicacion))
                } else
                {
                    console.error("Error eliminando la publicación")
                }
            })
            .catch(err => console.error(err))
    }

    const css = {
        container: {
            display: "flex",
            gap: "24px",
            padding: "24px",
            fontFamily: "Arial, sans-serif",
            backgroundColor: "#1a1a1a",
            minHeight: "100vh",
            color: "#fff"
        },
        leftCard: {
            flex: "0 0 250px",
            backgroundColor: "#111",
            borderRadius: "12px",
            padding: "20px",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            gap: "16px",
            boxShadow: "0 8px 24px rgba(0,0,0,0.4)",
            minHeight: "50vh"
        },
        userImage: {
            width: "120px",
            height: "120px",
            borderRadius: "50%",
            objectFit: "cover",
            backgroundColor: "#333"
        },
        userInfo: {
            width: "100%",
            display: "flex",
            flexDirection: "column",
            gap: "8px"
        },
        userLabel: {
            fontWeight: "500",
            color: "#aaa",
            fontSize: "14px"
        },
        userValue: {
            fontWeight: "600",
            fontSize: "16px"
        },
        mainContent: {
            flex: 1,
            display: "flex",
            flexDirection: "column",
            gap: "20px"
        },
        createButton: {
            width: "100%",
            padding: "16px",
            borderRadius: "12px",
            border: "none",
            backgroundColor: "#a7f3d0",
            color: "#000",
            fontWeight: "600",
            fontSize: "24px",
            cursor: "pointer"
        },
        pubCard: {
            display: "flex",
            gap: "16px",
            backgroundColor: "#111",
            borderRadius: "12px",
            padding: "16px",
            boxShadow: "0 6px 16px rgba(0,0,0,0.3)",
            alignItems: "flex-start"
        },
        pubImage: {
            width: "120px",
            height: "120px",
            borderRadius: "12px",
            objectFit: "cover",
            backgroundColor: "#333",
            flexShrink: 0
        },
        pubContentContainer: {
            display: "flex",
            flexDirection: "column",
            gap: "8px",
            flex: 1,
            position: "relative"
        },
        pubTitle: {
            fontSize: "18px",
            fontWeight: "600"
        },
        pubContent: {
            fontSize: "14px",
            lineHeight: "1.4",
            maxHeight: "100px",
            overflow: "hidden",
            position: "relative"
        },
        fade: {
            content: '""',
            position: "absolute",
            bottom: 0,
            left: 0,
            width: "100%",
            height: "24px",
            background: "linear-gradient(transparent, #111)"
        },
        pubDate: {
            fontSize: "12px",
            color: "#aaa"
        },
        pubButtons: {
            display: "flex",
            gap: "8px",
            marginTop: "8px"
        },
        pubButton: {
            padding: "6px 12px",
            borderRadius: "6px",
            border: "none",
            cursor: "pointer",
            fontWeight: "500",
            fontSize: "14px"
        },
        editButton: {
            backgroundColor: "#facc15",
            color: "#000"
        },
        viewButton: {
            backgroundColor: "#4ade80",
            color: "#000"
        },
        deleteButton: {
            backgroundColor: "#ef4444",
            color: "#fff"
        }
    }

    const isOwner = loggedUserId === id

    // aquí puedes mantener todo el CSS y la renderización igual
    return (
        <div style={css.container}>
            <div style={css.leftCard}>
                <img
                    src={usuario?.imagenBase64 ? `data:image/png;base64,${usuario.imagenBase64}` : "/placeholder-user.png"}
                    alt="Usuario"
                    style={css.userImage}
                />
                <div style={css.userInfo}>
                    <div style={css.userLabel}>Nombre</div>
                    <div style={css.userValue}>{usuario?.nombre || ""}</div>
                    <div style={css.userLabel}>Username</div>
                    <div style={css.userValue}>{usuario?.username || ""}</div>
                    <div style={css.userLabel}>Email</div>
                    <div style={css.userValue}>{usuario?.email || ""}</div>
                    {usuario?.telefono && (
                        <>
                            <div style={css.userLabel}>Teléfono</div>
                            <div style={css.userValue}>{usuario.telefono}</div>
                        </>
                    )}
                    {usuario?.direccion && (
                        <>
                            <div style={css.userLabel}>Dirección</div>
                            <div style={css.userValue}>{usuario.direccion}</div>
                        </>
                    )}
                </div>
            </div>
            <div style={css.mainContent}>
                {isOwner && (
                    <button style={css.createButton} onClick={() => navigate("/crearPublicacion")}>
                        +
                    </button>
                )}

                {publicaciones && publicaciones.length > 0 ? publicaciones.map(pub => (
                    <div key={pub.idPublicacion} style={css.pubCard}>
                        <img
                            src={pub.imagenBase64 ? `data:image/png;base64,${pub.imagenBase64}` : "/placeholder-user.png"}
                            alt="Publicación"
                            style={css.pubImage}
                        />
                        <div style={css.pubContentContainer}>
                            <div style={css.pubTitle}>{pub.titulo || "Sin título"}</div>
                            <div style={css.pubContent}>
                                {(pub.contenido || "").slice(0, 300)}
                                {(pub.contenido || "").length > 300 && <div style={css.fade}></div>}
                            </div>
                            <div style={css.pubDate}>
                                {pub.fechaCreacion ? new Date(pub.fechaCreacion).toLocaleDateString() : ""}
                            </div>
                            <div style={css.pubButtons}>
                                {isOwner && (
                                    <button
                                        style={{ ...css.pubButton, ...css.editButton }}
                                        onClick={() => navigate(`/editarPublicacion/${pub.idPublicacion}`)}
                                    >
                                        Editar
                                    </button>
                                )}
                                <button
                                    style={{ ...css.pubButton, ...css.viewButton }}
                                    onClick={() => navigate(`/publicacion/${pub.idPublicacion}`)}
                                >
                                    Ver Publicación
                                </button>
                                {isOwner && (
                                    <button
                                        style={{ ...css.pubButton, ...css.deleteButton }}
                                        onClick={() => handleDelete(pub.idPublicacion)}
                                    >
                                        Eliminar
                                    </button>
                                )}
                            </div>
                        </div>
                    </div>
                )) : <div style={{ color: "#aaa" }}>No hay publicaciones</div>}
            </div>
        </div>
    )
}
