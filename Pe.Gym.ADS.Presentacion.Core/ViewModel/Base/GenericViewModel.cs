using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GDirectiva.Presentacion.Core.ViewModel.Base
{
    /// <summary>
    /// Modelo de vista genérico - base
    /// </summary>
    /// <remarks>
    /// Creación:   GMD 20150327 <br />
    /// Modificación:            <br />
    /// </remarks>
    public abstract class GenericViewModel
    {

        /// <summary>
        /// Generar una lista de opciones con los valores de estado para las bandejas con filtro
        /// </summary>
        /// <returns>Lista generada</returns>
        protected List<SelectListItem> GenerarListadoOpcioneEstadoFiltro()
        {
            var opcionesEstado = GenerarListadoOpcioneEstado();
            return GenerarListadoOpcioneGenericoFiltro(opcionesEstado);
        }
        /// <summary>
        /// Generar una lista de opciones con los valores de estado para los furmularios
        /// </summary>
        /// <returns>Lista generada</returns>
        protected List<SelectListItem> GenerarListadoOpcioneEstadoFormulario()
        {
            var opcionesEstado = GenerarListadoOpcioneEstado();
            return GenerarListadoOpcioneGenericoFormulario(opcionesEstado);
        }
        /// <summary>
        /// Generar una lista de opciones con los valores de estado
        /// </summary>
        /// <returns>Lista generada</returns>
        private List<SelectListItem> GenerarListadoOpcioneEstado()
        {
            string etiquetaActiva = "true";
            string etiquetaInactiva = "false";

            //Estado de Unidad Operativa
            List<SelectListItem> estados = new List<SelectListItem>();
            estados.Add(new SelectListItem() { Value = "True", Text = etiquetaActiva });
            estados.Add(new SelectListItem() { Value = "False", Text = etiquetaInactiva });

            return estados;
        }


        /// <summary>
        /// Genera una lista de opciones con el item generico Todos
        /// </summary>
        /// <param name="opciones">Opciones a agregar</param>
        /// <returns>Lista generada</returns>
        protected List<SelectListItem> GenerarListadoOpcioneGenericoFiltro(List<SelectListItem> opciones = null)
        {
            return GenerarListadoOpcioneGenerico("--SELECCIONE--", opciones);
        }
        /// <summary>
        /// Genera una lista de opciones con el item generico Seleccionar
        /// </summary>
        /// <param name="opciones">Opciones a agregar</param>
        /// <returns>Lista generada</returns>
        protected List<SelectListItem> GenerarListadoOpcioneGenericoFormulario(List<SelectListItem> opciones = null)
        {
            return GenerarListadoOpcioneGenerico("--SELECCIONE--", opciones);
        }

        /// <summary>
        /// Genera una lista de opciones agregando la opción indicada
        /// </summary>
        /// <param name="etiquetaPrimaria">Etiqueta primaria o por default</param>
        /// <param name="opciones">Opciones a agregar</param>
        /// <returns>Lista de opciones</returns>
        protected List<SelectListItem> GenerarListadoOpcioneGenerico(string etiquetaPrimaria, List<SelectListItem> opciones = null)
        {
            List<SelectListItem> resultado = new List<SelectListItem>();
            bool seleccionado = true;
            if (opciones != null)
            {
                seleccionado = !opciones.Exists(i => i.Selected == true);
            }
            resultado.Add(new SelectListItem() { Value = "", Text = etiquetaPrimaria, Selected = seleccionado });
            if (opciones != null)
            {
                resultado.AddRange(opciones);
            }

            return resultado;
        }

        ///// <summary>
        ///// Genera una lista de opciones con el item generico Todos
        ///// </summary>
        ///// <param name="opciones">Opciones a agregar</param>
        ///// <param name="seleccionado">Item seleccionado</param>
        ///// <returns>Lista generada</returns>
        //protected List<SelectListItem> GenerarListadoOpcioneGenericoFiltro(List<CodigoValorResponse> opciones, object seleccionado = null)
        //{
        //    var opcionesCasteadas = CastearOpcioneGenerico(opciones, seleccionado);
        //    return GenerarListadoOpcioneGenericoFiltro(opcionesCasteadas);
        //}
        ///// <summary>
        ///// Genera una lista de opciones con el item generico Seleccionar
        ///// </summary>
        ///// <param name="opciones">Opciones a agregar</param>
        ///// <param name="seleccionado">Item seleccionado</param>
        ///// <returns>Lista generada</returns>
        //protected List<SelectListItem> GenerarListadoOpcioneGenericoFormulario(List<CodigoValorResponse> opciones, object seleccionado = null)
        //{
        //    var opcionesCasteadas = CastearOpcioneGenerico(opciones, seleccionado);
        //    return GenerarListadoOpcioneGenericoFormulario(opcionesCasteadas);
        //}
        ///// <summary>
        ///// Convierta una lista de CodigoValorResponse a SelectListItem
        ///// </summary>
        ///// <param name="opciones">Opciones a convertir</param>
        ///// <returns>Lista generada</returns>
        //private List<SelectListItem> CastearOpcioneGenerico(List<CodigoValorResponse> opciones, object seleccionado = null)
        //{
        //    List<SelectListItem> resultado = opciones.Select(i => new SelectListItem()
        //    {
        //        Value = i.Codigo.ToString(),
        //        Text = i.Valor.ToString(),
        //        Selected = seleccionado != null ? (i.Codigo.Equals(seleccionado)) : false
        //    }).ToList();
        //    return resultado;
        //}
    
    }
}
