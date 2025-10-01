import React from "react"
import { useParams } from "react-router-dom"

export default function Publicacion() {
  const { id } = useParams()
  return (
    <div>
      <h1>Publicación {id}</h1>
    </div>
  )
}
