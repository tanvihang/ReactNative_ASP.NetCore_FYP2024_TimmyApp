using Python.Runtime;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

namespace ConnectPython
{
	internal class Program
	{
		static void Main(string[] args)
		{
			CreateRunTime();

			string category = "mobile";
			string brand = "apple";
			string model = "iphone 14 pro";
			string[] spiders = { "mudah", "aihuishou" };
			int isTest = 0;
			int iteration = 25;

			RunFromScript(category,brand,model, spiders,isTest,iteration);

			//RunFromScriptDemo();

			ClosePythonRuntime();
		}

		static void CreateRunTime()
		{
			Runtime.PythonDLL = "C:\\Users\\tvh10\\AppData\\Local\\Programs\\Python\\Python310\\Python310.dll";
			PythonEngine.Initialize();
		}

		static void ClosePythonRuntime()
		{
			PythonEngine.Shutdown();
		}

		static void RunScript()
		{
			//create lock
			using (Py.GIL())
			{
				var pythonScript = Py.Import("APython");
				//pythonScript.InvokeMethod("printHello");

				var messageToPython = new PyString("It's from C# that get converted through PyString!");
				//pythonScript.InvokeMethod("printSomething", new PyObject[] {messageToPython});

				var ret = pythonScript.InvokeMethod("classReturn");
                Console.WriteLine(ret);
            }
		}

		static void RunScript2()
		{

			using (Py.GIL())
			{
				//adding the path to this project
				string pythonProjectPath = "D:\\UniversityFile\\Year4\\ScrapyFYP\\Server\\TestDifferentFolderScript";

				dynamic sys = Py.Import("sys");
				sys.path.append(pythonProjectPath);

				using (Py.GIL())
				{
					var pythonScript = Py.Import("CPython");
					pythonScript.InvokeMethod("CallAnotherPy");
				}

			}
		}

		static void RunScrapyCrawlerProcess(string searchTerm, string[] spiders)
		{
            Console.WriteLine(Directory.GetCurrentDirectory());
            string scrapyProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "Product");

			dynamic sys = Py.Import("sys");
			sys.path.append(scrapyProjectPath);

			using (Py.GIL())
			{
				dynamic crawlerProcessScript = Py.Import("Product.crawlerProcess");
				dynamic mainMethod = crawlerProcessScript.main;
				
				PyObject result = mainMethod(searchTerm,spiders);

				bool success = result.IsTrue();

                Console.WriteLine("Python method returned: " + success);

            }
		}

		static void RunFromScriptDemo()
		{
			string pythonInterpreter = "python"; // or specify the full path to the Python interpreter
			string pythonScript = "D:\\UniversityFile\\Year4\\ScrapyFYP\\Server\\Test\\ConnectPython\\APython.py";


			ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.FileName = pythonInterpreter;
			startInfo.Arguments = pythonScript;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.WorkingDirectory = "D:\\UniversityFile\\Year4\\ScrapyFYP\\Server\\Test\\ConnectPython";
		
			using(Process process = Process.Start(startInfo))
			{
				// Read the output and error streams asynchronously
				process.OutputDataReceived += (sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
					{
						string data = e.Data.Replace(" ", "");

						if (data.StartsWith("Output:"))
						{
							string result = data.Substring("Output:".Length);
                            Console.WriteLine("Output: " + data.Substring("Output:".Length));
							if(result == "success")
							{
                                Console.WriteLine("Run success");
                            }
                        }
                        Console.WriteLine(e.Data);
                        Console.WriteLine("--------------");
                    }
				};

				process.ErrorDataReceived += (sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
					{
						Console.WriteLine("Error: " + e.Data);
					}
				};

				// Begin asynchronous read operations
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();

				// Wait for the process to exit
				process.WaitForExit();

				// Output the exit code
				Console.WriteLine("Exit code: " + process.ExitCode);
			}
		}

		static void RunFromScript(string category, string brand, string model, string[] spiders, int isTest, int iteration)
		{
			// Define the path to the Python interpreter and the Python script
			string pythonInterpreter = "python"; // or specify the full path to the Python interpreter
			string pythonScript = "D:\\UniversityFile\\Year4\\ScrapyFYP\\Server\\Test\\ConnectPython\\Product\\Product\\crawlerProcess.py";

			// Create a StringBuilder to efficiently build the arguments string
			System.Text.StringBuilder argumentsBuilder = new System.Text.StringBuilder();
			argumentsBuilder.Append($"\"{pythonScript}\" -c \"{category}\" -b \"{brand}\" -m \"{model}\" -s");

			// Append each spider to the arguments string
			foreach (string spider in spiders)
			{
				argumentsBuilder.Append($" \"{spider}\"");
			}

			argumentsBuilder.Append($" -t {isTest}");
			argumentsBuilder.Append($" -i {iteration}");

			// Convert the StringBuilder to a string
			string arguments = argumentsBuilder.ToString();

            Console.WriteLine(arguments);

            // Create a new process start info
            ProcessStartInfo startInfo = new ProcessStartInfo();
			startInfo.FileName = pythonInterpreter;
			startInfo.Arguments = arguments;
			startInfo.UseShellExecute = false;
			startInfo.RedirectStandardOutput = true;
			startInfo.RedirectStandardError = true;
			startInfo.WorkingDirectory = "D:\\UniversityFile\\Year4\\ScrapyFYP\\Server\\Test\\ConnectPython\\Product\\";

			// Start the process
			using (Process process = Process.Start(startInfo))
			{
				// Read the output and error streams asynchronously
				process.OutputDataReceived += (sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
					{
						string data = e.Data.Replace(" ", "");

						if (e.Data == "success")
						{
							Console.WriteLine("Ran succeed");
						}

						Console.WriteLine(e.Data);
						Console.WriteLine("--------------");
					}
				};

				process.ErrorDataReceived += (sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
					{
						Console.WriteLine("Error: " + e.Data);
					}
				};

				// Begin asynchronous read operations
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();

				// Wait for the process to exit
				process.WaitForExit();

				// Output the exit code
				Console.WriteLine("Exit code: " + process.ExitCode);
			}
		}

	}
}