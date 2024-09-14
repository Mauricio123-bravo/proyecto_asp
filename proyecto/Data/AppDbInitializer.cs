using Microsoft.EntityFrameworkCore;
using proyecto.Models;

namespace proyecto.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                //Users
                if (!context.User.Any())
                {
                    context.User.AddRange(new List<User>()
                    {
                        new User()
                        {
                            Fullname = "Leidy Tatiana Alzate",
                            Email = "leidy.jimenez@example.com",
                            Address = "Calle 6 N 14-69",
                            Phone = "3124455878",
                            Document = "1002145874",
                            Password = "0000",
                        },
                        new User()
                        {
                            Fullname = "Jissel Melissa Suarez",
                            Email = "jissel.suarez@example.com",
                            Address = "Calle 9 N 14-69",
                            Phone = "3144458963",
                            Document = "1002155874",
                            Password = "1111",
                        },
                        new User()
                        {
                            Fullname = "Franyer Jefrey Rojas",
                            Email = "franyer.rojas@example.com",
                            Address = "Calle 10 N 10B-14",
                            Phone = "3144455878",
                            Document = "1002155874",
                            Password = "1212",
                        },
                    });
                    context.SaveChanges();
                }

                //Category

                if (!context.Category.Any())
                {
                    context.Category.AddRange(new List<Category>()
                    {
                    new Category()

                    {
                        Name="Petition",

                    },
                    new Category()
                    {
                        Name="Complaint",

                    },
                    new Category()
                    {
                        Name="Claim",

                    },
                    new Category()
                    {
                        Name="Suggestion",

                    }
                    });

                    context.SaveChanges();
                }

                //State
                if (!context.State.Any())
                {
                    context.State.AddRange(new List<State>()
                    {
                        new State()
                        {
                            Name = "Recibido"
                        },
                        new State()
                        {
                            Name = "En revisión"
                        },
                        new State()
                        {
                            Name = "Resuelto"
                        },
                        new State()
                        {
                            Name = "Rechazado"
                        },
                    });
                    context.SaveChanges();
                }

                //Departament
                if (!context.Departament.Any())
                {
                    context.Departament.AddRange(new List<Departament>()
                    {
                        new Departament()
                        {
                            Name = "Admisiones"
                        },
                        new Departament()
                        {
                            Name = "Cultura Fisaca"
                        },
                        new Departament()
                        {
                            Name = "Ingenierias"
                        },
                        new Departament()
                        {
                            Name = "Ciencias Basicas"
                        },
                    });
                    context.SaveChanges();
                }


                //CareStaff
                if (!context.CareStaff.Any())
                {
                    context.CareStaff.AddRange(new List<CareStaff>()
                    {
                        new CareStaff()
                        {
                            Name = "Leidy Johanna Jimenez",
                            Password = "password123",
                            Email = "leidy.jimenez@example.com",
                            Iddepartament = 1,
                        },
                        new CareStaff()
                        {
                            Name = "Juan Pérez",
                            Password = "password123",
                            Email = "juan.perez@example.com",
                            Iddepartament = 2,
                        },
                        new CareStaff()
                        {
                            Name = "Carlos Rodríguez",
                            Password = "password123",
                            Email = "carlos.rodriguez@example.com",
                            Iddepartament = 1,
                        },
                    });
                    context.SaveChanges();
                }

                //Pqrs

                if (!context.Pqrs.Any())
                {
                    context.Pqrs.AddRange(new List<Pqrs>()
                    {
                    new Pqrs()

                    {
                        Creation_date= DateTime.Now,
                        Description = "Buenos dias, me gustaria saber mas de los programas a distancia de la universidad santo tomas de tunja",
                        Code = GenerateUniqueCode(context),
                        Iduser=1,
                        Idcategory=1


                    },
                    new Pqrs()
                    {
                        Creation_date= DateTime.Now,
                        Description = "Buenos dias, quisiera sugerir una mejora en la atencion al cliente,gracias",
                        Code = GenerateUniqueCode(context),
                        Iduser=2,
                        Idcategory=4
                     }
                    });

                    context.SaveChanges();
                }

                //Followup
                if (!context.Followup.Any())
                {
                    context.Followup.AddRange(new List<Followup>()
                    {
                        new Followup()
                        {
                            Description = "Consulta inicial",
                            Date = DateTime.Now,
                            Idcarestaff = 3,
                            Idstate = 1,
                            Idpqrs = 1,
                        },
                        new Followup()
                        {
                            Description = "Chequeo rutinario",
                            Date = DateTime.Now,
                            Idcarestaff = 3,
                            Idstate = 3,
                            Idpqrs = 2,
                        },
                        new Followup()
                        {
                            Description = "Referencia a especialista",
                            Date = DateTime.Now,
                            Idcarestaff = 2,
                            Idstate = 2,
                            Idpqrs = 1,
                        },
                        new Followup()
                        {
                            Description = "Reasignacion a nuevo trabajador",
                            Date = DateTime.Now,
                            Idcarestaff = 1,
                            Idstate = 4,
                            Idpqrs = 2,
                        },
                    });
                    context.SaveChanges();
                }

                

                //Answer

                if (!context.Answer.Any())
                {
                    context.Answer.AddRange(new List<Answer>()
                    {
                    new Answer()

                    {
                        Content="buenos dias, claro que si la universidad ofrece varios programas a distancia entre esos esta ingenieria de sistemas",
                        Answer_date= DateTime.Now,
                        Idpqrs=1
                    },
                    new Answer()
                    {
                       Content="buenos dias, pedimos perdon por las molestias causadas y estaremos trabajando por mejorar nuestra atencion",
                        Answer_date= DateTime.Now,
                        Idpqrs=2
                     }
                    });

                    context.SaveChanges();
                }

               

            }
        }

        private static String GenerateUniqueCode(ApplicationDbContext context)
        {
            var random = new Random();
            String code;

            do
            {
                code = random.Next(100000000, 1000000000).ToString(); // Genera un número aleatorio de 9 dígitos

            }
            while (context.Pqrs.Any(p => p.Code == code)); // Verifica si el código ya existe en la base de datos

            return code;
        }
    }
    

}
