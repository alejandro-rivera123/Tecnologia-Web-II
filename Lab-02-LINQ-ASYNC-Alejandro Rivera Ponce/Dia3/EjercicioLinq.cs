using System;
using System.Collections.Generic;
using System.Linq;


namespace Dia3
{
    public class EjercicioLinq
    {
        public static void EjecutarTodos(List<Producto> productos)
        {
            Console.WriteLine("=======================EJERCICIO LINQ ========================\n");

            // ── NIVEL 1: Consultas básicas ──────────────────────────────────────

            // 1. Productos de "Tecnología" ordenados por precio mayor → menor
            Console.WriteLine("1. Tecnología (precio desc):");
            var tecnologiaDesc = productos
                .Where(p => p.Categoria == "Tecnología")
                .OrderByDescending(p => p.Precio);
            foreach (var p in tecnologiaDesc)
                Console.WriteLine($"   {p.Nombre} — ${p.Precio}");

            // 2. Productos con stock disponible → nombre y stock
            Console.WriteLine("\n2. Con stock disponible:");
            var conStock = productos
                .Where(p => p.Stock > 0)
                .Select(p => new { p.Nombre, p.Stock });
            foreach (var p in conStock)
                Console.WriteLine($"   {p.Nombre}: {p.Stock} unidades");

            // 3. Producto más barato y más caro
            Console.WriteLine("\n3. Extremos de precio:");
            var masBarato = productos.MinBy(p => p.Precio);
            var masCaro   = productos.MaxBy(p => p.Precio);
            Console.WriteLine($"   Más barato : {masBarato?.Nombre} (${masBarato?.Precio})");
            Console.WriteLine($"   Más caro   : {masCaro?.Nombre}   (${masCaro?.Precio})");

            // ── NIVEL 2: Consultas con cálculo ─────────────────────────────────

            // 4. Precio promedio de productos "Tecnología"
            Console.WriteLine("\n4. Precio promedio Tecnología:");
            var promTec = productos
                .Where(p => p.Categoria == "Tecnología")
                .Average(p => p.Precio);
            Console.WriteLine($"   ${promTec:F2}");

            // 5. Cantidad de productos por categoría
            Console.WriteLine("\n5. Productos por categoría:");
            var porCategoria = productos
                .GroupBy(p => p.Categoria)
                .Select(g => new { Categoria = g.Key, Cantidad = g.Count() });
            foreach (var c in porCategoria)
                Console.WriteLine($"   {c.Categoria}: {c.Cantidad}");

            // 6. Valor total del inventario (precio × stock)
            Console.WriteLine("\n6. Valor total del inventario:");
            var valorTotal = productos.Sum(p => p.Precio * p.Stock);
            Console.WriteLine($"   ${valorTotal:F2}");

            // ── NIVEL 3: Consultas combinadas ───────────────────────────────────

            // 7. Con stock, ordenados por valor inventario desc
            Console.WriteLine("\n7. Con stock — ordenados por valor de inventario desc:");
            var conStockOrdenado = productos
                .Where(p => p.Stock > 0)
                .Select(p => new
                {
                    p.Nombre,
                    p.Precio,
                    p.Stock,
                    ValorInventario = p.Precio * p.Stock
                })
                .OrderByDescending(x => x.ValorInventario);
            foreach (var p in conStockOrdenado)
                Console.WriteLine($"   {p.Nombre,-12} Precio:{p.Precio,6} Stock:{p.Stock,3} Valor:{p.ValorInventario,8}");

            // 8. ¿Existe producto en categoría con stock mínimo?
            Console.WriteLine("\n8. Verificación de existencia (Any):");
            bool caso1 = productos.Any(p => p.Categoria == "Tecnología" && p.Stock >= 20);
            bool caso2 = productos.Any(p => p.Categoria == "Muebles"    && p.Stock >= 10);
            Console.WriteLine($"   Tecnología + stock≥20 : {caso1}");  // true
            Console.WriteLine($"   Muebles    + stock≥10 : {caso2}");  // false

            Console.WriteLine();

            //lista ordenada
            var lista_ordenada= productos.OrderByDescending (x=>x.Precio);
            foreach(var item in lista_ordenada)
            {
                Console.WriteLine($"{item.Precio}");
            }
        }
    }
}
































