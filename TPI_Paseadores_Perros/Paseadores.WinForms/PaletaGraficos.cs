namespace Paseadores.WinForms
{
    // Colores de las series de los gráficos, siempre en el mismo orden.
    // El color sigue a la entidad: el paseador N de la lista siempre tiene el color N,
    // aunque cambie el filtro. Del noveno en adelante van todos juntos a "Otros".
    public static class PaletaGraficos
    {
        public static readonly string[] Series =
        {
            "#2a78d6", "#eb6834", "#1baf7a", "#eda100",
            "#e87ba4", "#008300", "#4a3aa7", "#e34948"
        };

        public const string Otros = "#9a9893";
    }
}
