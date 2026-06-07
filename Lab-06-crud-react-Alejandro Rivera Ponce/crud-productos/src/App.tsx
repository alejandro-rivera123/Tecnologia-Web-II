import { useState } from "react"

interface Producto {
  id: number
  nombre: string
  categoria: string
  precio: number
}

const datosIniciales: Producto[] = [
  { id: 1, nombre: "Laptop Lenovo IdeaPad", categoria: "Electrónica", precio: 4500 },
  { id: 2, nombre: "Silla Ergonómica", categoria: "Muebles", precio: 850 },
  { id: 3, nombre: "Teclado Mecánico", categoria: "Electrónica", precio: 320 },
  { id: 4, nombre: "Cuaderno Universitario", categoria: "Papelería", precio: 15 },
  { id: 5, nombre: "Mochila Ejecutiva", categoria: "Accesorios", precio: 180 },
]

const estilos = {
  body: {
    minHeight: "100vh",
    background: "linear-gradient(135deg, #0f0c29, #302b63, #24243e)",
    fontFamily: "'Segoe UI', sans-serif",
    padding: "2rem",
    color: "#e0e0e0",
  } as React.CSSProperties,
  tarjeta: {
    background: "rgba(255,255,255,0.05)",
    backdropFilter: "blur(12px)",
    border: "1px solid rgba(255,255,255,0.1)",
    borderRadius: "16px",
    padding: "2rem",
    maxWidth: "960px",
    margin: "0 auto",
    boxShadow: "0 8px 32px rgba(0,0,0,0.4)",
  } as React.CSSProperties,
  titulo: {
    fontSize: "2rem",
    fontWeight: 700,
    textAlign: "center" as const,
    marginBottom: "0.25rem",
    background: "linear-gradient(90deg, #a78bfa, #60a5fa)",
    WebkitBackgroundClip: "text",
    WebkitTextFillColor: "transparent",
    letterSpacing: "-0.5px",
  } as React.CSSProperties,
  subtitulo: {
    textAlign: "center" as const,
    fontSize: "0.85rem",
    color: "#94a3b8",
    marginBottom: "2rem",
  } as React.CSSProperties,
  formulario: {
    background: "rgba(255,255,255,0.03)",
    border: "1px solid rgba(255,255,255,0.08)",
    borderRadius: "12px",
    padding: "1.5rem",
    marginBottom: "2rem",
  } as React.CSSProperties,
  formTitulo: {
    fontSize: "1rem",
    fontWeight: 600,
    color: "#a78bfa",
    marginBottom: "1rem",
  } as React.CSSProperties,
  grid: {
    display: "grid",
    gridTemplateColumns: "1fr 1fr",
    gap: "0.75rem",
    marginBottom: "1rem",
  } as React.CSSProperties,
  label: {
    display: "block",
    fontSize: "0.75rem",
    color: "#94a3b8",
    marginBottom: "0.3rem",
    textTransform: "uppercase" as const,
    letterSpacing: "0.05em",
  } as React.CSSProperties,
  input: {
    width: "100%",
    padding: "0.6rem 0.9rem",
    background: "rgba(255,255,255,0.06)",
    border: "1px solid rgba(255,255,255,0.12)",
    borderRadius: "8px",
    color: "#e0e0e0",
    fontSize: "0.9rem",
    outline: "none",
    boxSizing: "border-box" as const,
  } as React.CSSProperties,
  btnPrimario: {
    padding: "0.6rem 1.4rem",
    background: "linear-gradient(135deg, #7c3aed, #4f46e5)",
    border: "none",
    borderRadius: "8px",
    color: "#fff",
    fontWeight: 600,
    fontSize: "0.9rem",
    cursor: "pointer",
    marginRight: "0.5rem",
  } as React.CSSProperties,
  btnSecundario: {
    padding: "0.6rem 1.4rem",
    background: "rgba(255,255,255,0.07)",
    border: "1px solid rgba(255,255,255,0.12)",
    borderRadius: "8px",
    color: "#94a3b8",
    fontWeight: 500,
    fontSize: "0.9rem",
    cursor: "pointer",
  } as React.CSSProperties,
  btnEditar: {
    padding: "0.35rem 0.8rem",
    background: "rgba(96,165,250,0.15)",
    border: "1px solid rgba(96,165,250,0.3)",
    borderRadius: "6px",
    color: "#60a5fa",
    fontSize: "0.8rem",
    cursor: "pointer",
    marginRight: "0.4rem",
  } as React.CSSProperties,
  btnEliminar: {
    padding: "0.35rem 0.8rem",
    background: "rgba(248,113,113,0.15)",
    border: "1px solid rgba(248,113,113,0.3)",
    borderRadius: "6px",
    color: "#f87171",
    fontSize: "0.8rem",
    cursor: "pointer",
  } as React.CSSProperties,
  tabla: {
    width: "100%",
    borderCollapse: "collapse" as const,
    fontSize: "0.9rem",
  } as React.CSSProperties,
  th: {
    textAlign: "left" as const,
    padding: "0.75rem 1rem",
    background: "rgba(167,139,250,0.1)",
    color: "#a78bfa",
    fontWeight: 600,
    fontSize: "0.75rem",
    textTransform: "uppercase" as const,
    letterSpacing: "0.06em",
    borderBottom: "1px solid rgba(255,255,255,0.08)",
  } as React.CSSProperties,
  td: {
    padding: "0.8rem 1rem",
    borderBottom: "1px solid rgba(255,255,255,0.05)",
    verticalAlign: "middle" as const,
  } as React.CSSProperties,
  badge: (cat: string): React.CSSProperties => {
    const colores: Record<string, string> = {
      Electrónica: "#60a5fa",
      Muebles: "#34d399",
      Papelería: "#fbbf24",
      Accesorios: "#f472b6",
    }
    const color = colores[cat] ?? "#a78bfa"
    return {
      display: "inline-block",
      padding: "0.2rem 0.65rem",
      borderRadius: "999px",
      fontSize: "0.75rem",
      fontWeight: 500,
      background: color + "20",
      color,
      border: `1px solid ${color}40`,
    }
  },
  total: {
    marginTop: "1.25rem",
    textAlign: "right" as const,
    fontSize: "0.85rem",
    color: "#94a3b8",
  } as React.CSSProperties,
  totalNum: {
    fontWeight: 700,
    color: "#a78bfa",
    fontSize: "1rem",
  } as React.CSSProperties,
  modalOverlay: {
    position: "fixed" as const,
    inset: 0,
    background: "rgba(0,0,0,0.65)",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    zIndex: 999,
  } as React.CSSProperties,
  modal: {
    background: "#1e1b4b",
    border: "1px solid rgba(255,255,255,0.1)",
    borderRadius: "14px",
    padding: "2rem",
    maxWidth: "360px",
    width: "90%",
    textAlign: "center" as const,
    boxShadow: "0 20px 60px rgba(0,0,0,0.6)",
  } as React.CSSProperties,
}

export default function App() {
  const [productos, setProductos] = useState<Producto[]>(datosIniciales)
  const [nombre, setNombre] = useState("")
  const [categoria, setCategoria] = useState("")
  const [precio, setPrecio] = useState("")
  const [idEditando, setIdEditando] = useState<number | null>(null)
  const [idAEliminar, setIdAEliminar] = useState<number | null>(null)

  const limpiarFormulario = () => {
    setNombre("")
    setCategoria("")
    setPrecio("")
    setIdEditando(null)
  }

  const handleGuardar = () => {
    if (!nombre.trim() || !categoria.trim() || !precio) {
      alert("Por favor completa todos los campos.")
      return
    }
    if (idEditando !== null) {
      setProductos(
        productos.map((p) =>
          p.id === idEditando
            ? { ...p, nombre, categoria, precio: Number(precio) }
            : p
        )
      )
    } else {
      const nuevoProducto: Producto = {
        id: Date.now(),
        nombre,
        categoria,
        precio: Number(precio),
      }
      setProductos([...productos, nuevoProducto])
    }
    limpiarFormulario()
  }

  const handleEditar = (p: Producto) => {
    setIdEditando(p.id)
    setNombre(p.nombre)
    setCategoria(p.categoria)
    setPrecio(String(p.precio))
    window.scrollTo({ top: 0, behavior: "smooth" })
  }

  const handleConfirmarEliminar = () => {
    if (idAEliminar !== null) {
      setProductos(productos.filter((p) => p.id !== idAEliminar))
      setIdAEliminar(null)
    }
  }

  return (
    <div style={estilos.body}>
      <div style={estilos.tarjeta}>
        <h1 style={estilos.titulo}>📦 Gestión de Productos</h1>
        <p style={estilos.subtitulo}>Lab-6</p>

        <div style={estilos.formulario}>
          <p style={estilos.formTitulo}>
            {idEditando !== null ? "✏️ Editar producto" : "➕ Nuevo producto"}
          </p>
          <div style={estilos.grid}>
            <div>
              <label style={estilos.label}>Nombre</label>
              <input
                style={estilos.input}
                placeholder='Ej: Monitor LG 27"'
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
              />
            </div>
            <div>
              <label style={estilos.label}>Categoría</label>
              <input
                style={estilos.input}
                placeholder="Ej: Electrónica"
                value={categoria}
                onChange={(e) => setCategoria(e.target.value)}
              />
            </div>
            <div>
              <label style={estilos.label}>Precio (Bs.)</label>
              <input
                style={estilos.input}
                type="number"
                placeholder="0.00"
                value={precio}
                onChange={(e) => setPrecio(e.target.value)}
              />
            </div>
          </div>
          <button style={estilos.btnPrimario} onClick={handleGuardar}>
            {idEditando !== null ? "Guardar cambios" : "Agregar producto"}
          </button>
          {idEditando !== null && (
            <button style={estilos.btnSecundario} onClick={limpiarFormulario}>
              Cancelar
            </button>
          )}
        </div>

        <div style={{ overflowX: "auto" }}>
          <table style={estilos.tabla}>
            <thead>
              <tr>
                <th style={estilos.th}>#</th>
                <th style={estilos.th}>Nombre</th>
                <th style={estilos.th}>Categoría</th>
                <th style={estilos.th}>Precio</th>
                <th style={{ ...estilos.th, textAlign: "center" }}>Acciones</th>
              </tr>
            </thead>
            <tbody>
              {productos.map((p, i) => (
                <tr
                  key={p.id}
                  style={{
                    background: idEditando === p.id ? "rgba(167,139,250,0.06)" : "transparent",
                  }}
                >
                  <td style={{ ...estilos.td, color: "#64748b", width: "40px" }}>{i + 1}</td>
                  <td style={{ ...estilos.td, fontWeight: 500 }}>{p.nombre}</td>
                  <td style={estilos.td}>
                    <span style={estilos.badge(p.categoria)}>{p.categoria}</span>
                  </td>
                  <td style={{ ...estilos.td, fontWeight: 600, color: "#34d399" }}>
                    Bs. {p.precio.toFixed(2)}
                  </td>
                  <td style={{ ...estilos.td, textAlign: "center" }}>
                    <button style={estilos.btnEditar} onClick={() => handleEditar(p)}>Editar</button>
                    <button style={estilos.btnEliminar} onClick={() => setIdAEliminar(p.id)}>Eliminar</button>
                  </td>
                </tr>
              ))}
              {productos.length === 0 && (
                <tr>
                  <td colSpan={5} style={{ ...estilos.td, textAlign: "center", color: "#64748b", padding: "2rem" }}>
                    No hay productos registrados.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        <p style={estilos.total}>
          Total de productos registrados:{" "}
          <span style={estilos.totalNum}>{productos.length}</span>
        </p>
      </div>

      {idAEliminar !== null && (
        <div style={estilos.modalOverlay}>
          <div style={estilos.modal}>
            <p style={{ fontSize: "2rem", marginBottom: "0.5rem" }}>🗑️</p>
            <p style={{ fontWeight: 700, fontSize: "1.1rem", marginBottom: "0.5rem" }}>
              ¿Eliminar producto?
            </p>
            <p style={{ color: "#94a3b8", fontSize: "0.9rem", marginBottom: "1.5rem" }}>
              Esta acción no se puede deshacer.
            </p>
            <button
              style={{ ...estilos.btnEliminar, padding: "0.6rem 1.4rem", marginRight: "0.5rem" }}
              onClick={handleConfirmarEliminar}
            >
              Sí, eliminar
            </button>
            <button style={estilos.btnSecundario} onClick={() => setIdAEliminar(null)}>
              Cancelar
            </button>
          </div>
        </div>
      )}
    </div>
  )
}