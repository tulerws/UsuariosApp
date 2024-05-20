using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento ORM para usuário
    /// </summary>
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //mapeamento da tabela
            builder.ToTable("USUARIO");

            //mapeamento da chave primária
            builder.HasKey(u => u.Id);

            //mapeamento dos campos da tabela
            builder.Property(u => u.Id).HasColumnName("ID");
            builder.Property(u => u.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            builder.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(100).IsRequired();
            builder.Property(u => u.Senha).HasColumnName("SENHA").HasMaxLength(100).IsRequired();
            builder.Property(u => u.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();
            builder.Property(u => u.PerfilId).HasColumnName("PERFIL_ID");

            //criando um índice para tornar o campo Email único
            builder.HasIndex(u => u.Email).IsUnique();

            //mapeamento do relacionamento de 1 para muitos
            builder.HasOne(u => u.Perfil) //Usuário TEM 1 Perfil
                .WithMany(p => p.Usuarios) //Perfil TEM MUITOS Usuários
                .HasForeignKey(u => u.PerfilId); //Chave estrangeira
        }
    }
}
