using MediCare.Data;
using MediCare.Dtos;
using MediCare.Models;
using MediCare_.DTOs;
using MediCare_.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediCare.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGenericService<Role> _roleService;

        public RolesController(IGenericService<Role> roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("filtersearch")]
        public async Task<IActionResult> SearchUsersByFilters([FromBody] List<Filter> filters)
        {
            //if (filters == null || !filters.Any())
            //{
            //    return BadRequest(new { Message = "At least one filter must be provided" });
            //}

            var roles = await _roleService.GetByMultipleConditionsAsync(filters);

            if (!roles.Any())
            {
                return NotFound(new { Message = "No role found matching the criteria" });
            }

            return Ok(roles);
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        //{
        //    var roles = await _roleService.GetAllAsync();
        //    return Ok(roles);
        //}

        //// 🔹 2. Get Role By ID
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Role>> GetRole(int id)
        //{
        //    var role = await _roleService.GetByIdAsync(id);
        //    if (role == null) return NotFound("Role not found");
        //    return role;
        //}

        // 🔹 3. Create Role
        [HttpPost]
        public async Task<ActionResult<Role>> CreateRole([FromBody] RoleDto roleDto)
        {
            if (string.IsNullOrWhiteSpace(roleDto.RoleName))
                return BadRequest("Role name is required");

            var role = new Role { RoleName = roleDto.RoleName };
            await _roleService.AddAsync(role);
            return Ok(new { Message = "Role created successfully", Specialization = role });
        }

        // 🔹 4. Update Role
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDto roleDto)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound("Role not found");
            role.RoleName = roleDto.RoleName ?? role.RoleName;
            await _roleService.UpdateAsync(role);
            return Ok(new { message = "Role updated successfully" });
        }

        // 🔹 5. Delete Role
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound("Role not found");
            await _roleService.DeleteAsync(id);
            return Ok(new { message = "Role deleted successfully" });
        }
    }
}