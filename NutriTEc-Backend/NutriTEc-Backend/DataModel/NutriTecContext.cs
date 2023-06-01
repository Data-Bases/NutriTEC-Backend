using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NutriTEc_Backend.Models;

namespace NutriTEc_Backend.DataModel;

public partial class NutriTecContext : DbContext
{
    public NutriTecContext()
    {
    }

    public NutriTecContext(DbContextOptions<NutriTecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Chargetype> Chargetypes { get; set; }

    public virtual DbSet<Measurement> Measurements { get; set; }

    public virtual DbSet<Nutritionist> Nutritionists { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Patientproduct> Patientproducts { get; set; }

    public virtual DbSet<Patientrecipe> Patientrecipes { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Planpatient> Planpatients { get; set; }

    public virtual DbSet<Planproduct> Planproducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productrecipe> Productrecipes { get; set; }

    public virtual DbSet<Productvitamin> Productvitamins { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Usercredential> Usercredentials { get; set; }

    public virtual DbSet<Vitamin> Vitamins { get; set; }

    public virtual DbSet<RecipeId> RecipeIds { get; set; }
    public virtual DbSet<RecipeNutrients> RecipeNutrients { get; set; }
    public virtual DbSet<ProductRecipeNutrients> ProductRecipeNutrients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=nutritec.postgres.database.azure.com;Database= NutriTEc;Port=5432;User Id=diani@nutritec;Password=Pepe!bobby;Ssl Mode=Require; Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
<<<<<<< HEAD:NutriTEc-Backend/NutriTEc-Backend/Repository/DataModel/NutriTecContext.cs
=======

        modelBuilder.Entity<RecipeId>().HasNoKey();
        modelBuilder.Entity<RecipeNutrients>().HasNoKey();
        modelBuilder.Entity<ProductRecipeNutrients>().HasNoKey();

>>>>>>> f62ae96398460b5c584351073b6fdd8c8edd2f77:NutriTEc-Backend/NutriTEc-Backend/DataModel/NutriTecContext.cs
        modelBuilder
            .HasPostgresExtension("pg_buffercache")
            .HasPostgresExtension("pg_stat_statements");

        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrator_pkey");

            entity.ToTable("administrator");

            entity.HasIndex(e => e.Email, "administrator_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Chargetype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chargetype_pkey");

            entity.ToTable("chargetype");

            entity.HasIndex(e => e.Nombre, "chargetype_nombre_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Measurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("measurements_pkey");

            entity.ToTable("measurements");

            entity.HasIndex(e => new { e.Patientid, e.Revisiondate }, "measurements_patientid_revisiondate_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fatpercentage).HasColumnName("fatpercentage");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Hips).HasColumnName("hips");
            entity.Property(e => e.Musclepercentage).HasColumnName("musclepercentage");
            entity.Property(e => e.Neck).HasColumnName("neck");
            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Revisiondate).HasColumnName("revisiondate");
            entity.Property(e => e.Waist).HasColumnName("waist");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Patient).WithMany(p => p.Measurements)
                .HasForeignKey(d => d.Patientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("measurements_patientid");
        });

        modelBuilder.Entity<Nutritionist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutritionist_pkey");

            entity.ToTable("nutritionist");

            entity.HasIndex(e => e.Email, "nutritionist_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Adminid).HasColumnName("adminid");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Canton)
                .HasMaxLength(100)
                .HasColumnName("canton");
            entity.Property(e => e.Cardnumber).HasColumnName("cardnumber");
            entity.Property(e => e.Chargetypeid).HasColumnName("chargetypeid");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Imc).HasColumnName("imc");
            entity.Property(e => e.Lastname1)
                .HasMaxLength(100)
                .HasColumnName("lastname1");
            entity.Property(e => e.Lastname2)
                .HasMaxLength(100)
                .HasColumnName("lastname2");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Nutritionistcode).HasColumnName("nutritionistcode");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Picture)
                .HasMaxLength(100)
                .HasColumnName("picture");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .HasColumnName("province");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Admin).WithMany(p => p.Nutritionists)
                .HasForeignKey(d => d.Adminid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutritionist_adminemail");

            entity.HasOne(d => d.Chargetype).WithMany(p => p.Nutritionists)
                .HasForeignKey(d => d.Chargetypeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutritionist_chargetypeid");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_pkey");

            entity.ToTable("patient");

            entity.HasIndex(e => e.Email, "patient_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Caloriesintake).HasColumnName("caloriesintake");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Lastname1)
                .HasMaxLength(100)
                .HasColumnName("lastname1");
            entity.Property(e => e.Lastname2)
                .HasMaxLength(100)
                .HasColumnName("lastname2");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Nutriid).HasColumnName("nutriid");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");

            entity.HasOne(d => d.Nutri).WithMany(p => p.Patients)
                .HasForeignKey(d => d.Nutriid)
                .HasConstraintName("patient_nutriid");
        });

        modelBuilder.Entity<Patientproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patientproduct_pkey");

            entity.ToTable("patientproduct");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Consumedate).HasColumnName("consumedate");
            entity.Property(e => e.Mealtime)
                .HasMaxLength(50)
                .HasColumnName("mealtime");
            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Productbarcode).HasColumnName("productbarcode");

            entity.HasOne(d => d.Patient).WithMany(p => p.Patientproducts)
                .HasForeignKey(d => d.Patientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patientproduct_patientid");

            entity.HasOne(d => d.ProductbarcodeNavigation).WithMany(p => p.Patientproducts)
                .HasForeignKey(d => d.Productbarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patientproduct_productbarcode");
        });

        modelBuilder.Entity<Patientrecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patientrecipe_pkey");

            entity.ToTable("patientrecipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Consumedate).HasColumnName("consumedate");
            entity.Property(e => e.Mealtime)
                .HasMaxLength(50)
                .HasColumnName("mealtime");
            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Recipeid).HasColumnName("recipeid");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientrecipePatients)
                .HasForeignKey(d => d.Patientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patientrecipe_patientid");

            entity.HasOne(d => d.Recipe).WithMany(p => p.PatientrecipeRecipes)
                .HasForeignKey(d => d.Recipeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patientrecipe_recipeid");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plan_pkey");

            entity.ToTable("plan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Nutriid).HasColumnName("nutriid");

            entity.HasOne(d => d.Nutri).WithMany(p => p.Plans)
                .HasForeignKey(d => d.Nutriid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_nutriid");
        });

        modelBuilder.Entity<Planpatient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planpatient_pkey");

            entity.ToTable("planpatient");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Initialdate).HasColumnName("initialdate");
            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Planid).HasColumnName("planid");

            entity.HasOne(d => d.Patient).WithMany(p => p.Planpatients)
                .HasForeignKey(d => d.Patientid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("planpatient_patientid");

            entity.HasOne(d => d.Plan).WithMany(p => p.Planpatients)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("planpatient_planid");
        });

        modelBuilder.Entity<Planproduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("planproduct_pkey");

            entity.ToTable("planproduct");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Consumeweekday)
                .HasMaxLength(50)
                .HasColumnName("consumeweekday");
            entity.Property(e => e.Mealtime)
                .HasMaxLength(50)
                .HasColumnName("mealtime");
            entity.Property(e => e.Planid).HasColumnName("planid");
            entity.Property(e => e.Productbarcode).HasColumnName("productbarcode");
            entity.Property(e => e.Servings).HasColumnName("servings");

            entity.HasOne(d => d.Plan).WithMany(p => p.Planproducts)
                .HasForeignKey(d => d.Planid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("planproduct_planid");

            entity.HasOne(d => d.ProductbarcodeNavigation).WithMany(p => p.Planproducts)
                .HasForeignKey(d => d.Productbarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("planproduct_productbarcode");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Barcode).HasName("product_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Barcode)
                .ValueGeneratedNever()
                .HasColumnName("barcode");
            entity.Property(e => e.Calcium).HasColumnName("calcium");
            entity.Property(e => e.Carbs).HasColumnName("carbs");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .HasColumnName("descripcion");
            entity.Property(e => e.Energy).HasColumnName("energy");
            entity.Property(e => e.Fat).HasColumnName("fat");
            entity.Property(e => e.Iron).HasColumnName("iron");
            entity.Property(e => e.Isapproved).HasColumnName("isapproved");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Portionsize).HasColumnName("portionsize");
            entity.Property(e => e.Protein).HasColumnName("protein");
            entity.Property(e => e.Sodium).HasColumnName("sodium");
        });

        modelBuilder.Entity<Productrecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productrecipe_pkey");

            entity.ToTable("productrecipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Productbarcode).HasColumnName("productbarcode");
            entity.Property(e => e.Recipeid).HasColumnName("recipeid");
            entity.Property(e => e.Servings).HasColumnName("servings");

            entity.HasOne(d => d.ProductbarcodeNavigation).WithMany(p => p.Productrecipes)
                .HasForeignKey(d => d.Productbarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productrecipe_barcode");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Productrecipes)
                .HasForeignKey(d => d.Recipeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productrecipe_recipeid");
        });

        modelBuilder.Entity<Productvitamin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productvitamin_pkey");

            entity.ToTable("productvitamin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Productbarcode).HasColumnName("productbarcode");
            entity.Property(e => e.Vitaminid).HasColumnName("vitaminid");

            entity.HasOne(d => d.ProductbarcodeNavigation).WithMany(p => p.Productvitamins)
                .HasForeignKey(d => d.Productbarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productvitamin_productbarcode");

            entity.HasOne(d => d.Vitamin).WithMany(p => p.Productvitamins)
                .HasForeignKey(d => d.Vitaminid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productvitamin_vitamin");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_pkey");

            entity.ToTable("recipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Usercredential>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("usercredentials");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Usertype).HasColumnName("usertype");
        });

        modelBuilder.Entity<Vitamin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vitamin_pkey");

            entity.ToTable("vitamin");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
