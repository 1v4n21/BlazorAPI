<h1>Blazor Project with C# API</h1> <p>This project is a web application developed with Blazor that uses WebAssembly for the client-side and a C# API for connecting to the database. It provides a modern and efficient architecture for building interactive and scalable web applications.</p> <h2>Prerequisites</h2> <ul> <li><a href="https://dotnet.microsoft.com/download" target="_blank">.NET Core SDK</a> >= 5.0</li> <li>Visual Studio or <a href="https://code.visualstudio.com/" target="_blank">Visual Studio Code</a> (optional)</li> </ul> <h2>Project Setup</h2> <ol> <li>Clone this repository to your local machine:<br><code>git clone https://github.com/1v4n21/BlazorAPI.git</code></li> <li>Navigate to the project directory:<br><code>cd yourproject</code></li> <li>Run the application:<br><code>dotnet run</code></li> <li>Open your browser and navigate to <a href="https://localhost:5001" target="_blank">https://localhost:5001</a> to see the application in action.</li> </ol> <h2>Project Structure</h2> <p>The project is structured as follows:</p> <ul> <li><strong>ClientApp:</strong> Contains the client-side Blazor code developed with WebAssembly.</li> <li><strong>Server:</strong> Contains the C# API that communicates with the database.</li> <li><strong>Shared:</strong> Contains shared code between the client and server.</li> </ul> <h2>Database Configuration</h2> <p>To configure the connection to your database, follow these steps:</p> <ol> <li>Open the <code>appsettings.json</code> file in the API project (<code>Server</code>) and modify the connection string according to your database configuration.</li> <li>Run the migrations to create the necessary tables in your database:<br><code>dotnet ef database update</code></li> </ol> <h2>Contribution</h2> <p>If you want to contribute to this project, please follow these steps:</p> <ol> <li>Fork the repository.</li> <li>Create a new branch (<code>git checkout -b feature/feature-name</code>).</li> <li>Make your changes and commit (<code>git commit -am 'Add new feature'</code>).</li> <li>Push to the branch (<code>git push origin feature/feature-name</code>).</li> <li>Create a pull request.</li> </ol> <h2>Issues</h2> <p>If you encounter any problems or have any questions, please open an issue in this repository.</p>
