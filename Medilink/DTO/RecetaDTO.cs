namespace Medilink.DTO;
public record RecetaEnvioDto(
    string Token,
    DateTime Timestamp,
    RecetaDataDto Receta
);

public record RecetaDataDto(
    int Id,
    MedicoRecetaDto Medico,
    PacienteRecetaDto Paciente,
    List<MedicamentoRecetaDto> Medicamentos,
    DateTime FechaVencimiento
);

public record MedicoRecetaDto(
    string Matricula,
    string Nombre,
    string Apellido,
    string DNI
);

public record PacienteRecetaDto(
    string DNI,
    string Nombre,
    string Apellido
);

public record MedicamentoRecetaDto(
    string Nombre,
    int Cantidad,
    string Descripcion
);