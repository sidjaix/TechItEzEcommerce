# Use the Azure SQL Edge base image
FROM mcr.microsoft.com/azure-sql-edge

# Set environment variables for SQL Server
ENV SA_PASSWORD=Admin@123
ENV ACCEPT_EULA=Y

# Create a directory for your scripts (this will be mapped to a volume)
RUN mkdir -p /var/opt/mssql/scripts

# Copy your SQL script into the container's volume
#COPY your-script.sql /var/opt/mssql/scripts/

# Define a volume to persist data
VOLUME /var/opt/mssql

# Expose the default SQL Server port
EXPOSE 1433

# Optional: Run your script after container starts
# Uncomment the next line if you want to run the script immediately after starting the container
# CMD /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Admin@123" -i /var/opt/mssql/scripts/your-script.sql
