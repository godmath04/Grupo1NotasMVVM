using Grupo1NotasMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grupo1NotasMVVM.Services
{
    public class RecordatorioService
    {
        private const string FileName = "recordatorios.json";
        private string FilePath => Path.Combine(FileSystem.AppDataDirectory, FileName);

        public async Task<List<Recordatorio>> CargarAsync()
        {
            if (!File.Exists(FilePath))
                return new List<Recordatorio>();

            using var stream = File.OpenRead(FilePath);
            return await JsonSerializer.DeserializeAsync<List<Recordatorio>>(stream) ?? new List<Recordatorio>();
        }

        public async Task GuardarAsync(List<Recordatorio> recordatorios)
        {
            using var stream = File.Create(FilePath);
            await JsonSerializer.SerializeAsync(stream, recordatorios);
        }
    }
}
