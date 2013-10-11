using System;
using System.Collections.Generic;
using System.Text;
using EstacionDB.DAO;
using EstacionDB.DTO;
using EstacionDB.VO;
using EstacionDB.Exceptions;

namespace EstacionDB.Core
{   
    public class UsuarioCore
    {
        //Atributos
        private static volatile UsuarioCore instance;
        private UsuarioDAO dao;
        //Constructor
        private UsuarioCore()
        {
            dao = new UsuarioDAO();
        }
        //Métodos
        /// <summary>
        /// Obtiene la instancia de UsuarioCore
        /// </summary>
        /// <returns>Instancia de UsuarioCore</returns>
        public static UsuarioCore getInstance()
        {
            if (instance == null)
            {
                instance = new UsuarioCore();
            }
            return instance;
        }
        /// <summary>
        /// <para>Valida el usuario a partir del nombre de usuario y la contraseña.</para>
        /// <para>UsuarioException: Si el usuario no existe.</para>
        /// <para>UsuarioException: Si la operación falla.</para>
        /// </summary>
        /// <param name="usuario">Nombre o ID del usuario.</param>
        /// <param name="pwd">Texto sin encriptar del password.</param>
        /// <returns>Objeto dto con la información del usuario.</returns>
        public UsuarioDTO validarUsuario(string usuario, string pwd)
        {
            UsuarioVO usuarioValidado = null;
            UsuarioDTO dto = null;
            try
            {
                usuarioValidado = dao.consultarUsuario(usuario, EstacionDB.Security.Security.createHash(pwd));
                if (usuarioValidado != null)
                {
                    dto = new UsuarioDTO();
                    dto.Usuario = usuarioValidado.IdUsuario;
                    dto.IsAdmin = usuarioValidado.IsAdmin;
                    //El password no se retorna en esta parte.
                }
                else
                {
                    throw new UsuarioException("Usuario no válido o no existe.");
                }
            }
            catch (EstacionDBException ex)
            {
                throw new UsuarioException("No se puede validar la información de usuario en la base de datos", ex);
            }
            
            return dto;
        }
        /// <summary>
        /// <para>Guarda un nuevo usuario en la base de datos.</para>
        /// <para>UsuarioException: Si el usuario ya existe.</para>
        /// <para>UsuarioException: Si falla la operación.</para>
        /// </summary>
        /// <param name="dto">Objeto dto con los datos del usuario a crear.</param>
        /// <returns>1 Si se guardó el usuario; Cero si no.</returns>
        public int crearUsuario(UsuarioDTO dto)
        {
            try
            {
                UsuarioVO vo = dao.consultarUsuario(dto.Usuario);
                if (vo != null)
                {
                    throw new UsuarioException("Usuario ya existe");
                }
                else
                {
                    vo = new UsuarioVO();
                    vo.IdUsuario = dto.Usuario;
                    vo.Pwd = EstacionDB.Security.Security.createHash(dto.Pwd);
                    vo.IsAdmin = dto.IsAdmin;

                    return dao.guardarUsuario(vo);
                }
            }
            catch (EstacionDBException ex)
            {
                throw new UsuarioException("No se pudo agregar el usuario en la base de datos", ex);
            }
        }

        /// <summary>
        /// <para>Actualiza el password y el tipo de usuario (Admin o no Admin).</para>
        /// <para>UsuarioException: Si el usuario no existe.</para>
        /// <para>UsuarioException: Si falla la operación.</para>
        /// </summary>
        /// <param name="dto">Objeto dto con los datos del usuario a actualizar.</param>
        /// <returns>1 Si se guardó el usuario; Cero si no.</returns>
        public int actualizarUsuario(UsuarioDTO dto)
        {
            try
            {
                UsuarioVO vo = dao.consultarUsuario(dto.Usuario);
                if (vo == null)
                {
                    throw new UsuarioException("Usuario no existe");
                }
                else
                {   
                    vo.IdUsuario = dto.Usuario;
                    vo.Pwd = EstacionDB.Security.Security.createHash(dto.Pwd);
                    vo.IsAdmin = dto.IsAdmin;

                    return dao.guardarUsuario(vo);
                }
            }
            catch (EstacionDBException ex)
            {
                throw new UsuarioException("No se pudo actualizar el usuario en la base de datos", ex);
            }
        }
        /// <summary>
        /// <para>Consulta todos los usuarios</para>
        /// </summary>
        /// <returns>Lista de dto's con la información de los usuarios</returns>
        public List<UsuarioDTO> consultarUsuarios()
        {
            try
            {
                List<UsuarioDTO> dtos = new List<UsuarioDTO>();
                List<UsuarioVO> vos = dao.consultarUsuarios();

                foreach (UsuarioVO vo in vos)
                {
                    dtos.Add(new UsuarioDTO(vo.IdUsuario, vo.IsAdmin));
                }

                return dtos;
            }
            catch (EstacionDBException ex)
            {
                throw new UsuarioException("No se pudo acceder a la lista de usuarios en la base de datos", ex);
            }
        }
    }
}