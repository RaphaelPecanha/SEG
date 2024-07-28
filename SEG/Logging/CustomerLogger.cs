namespace SEG.Logging;

public class CustomerLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        // Use um caminho relativo em vez de um caminho absoluto
        string caminhoRelativo = "C:\\Users\\raphael.pecanha\\Desktop\\Projetos Raphael\\Site\\API\\SEG\\SEG\\Logging\\Log.txt";
        string caminhoArquivoLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, caminhoRelativo);

        using (StreamWriter streamWriter = new StreamWriter(caminhoRelativo, true))
        {
            try
            {
                streamWriter.WriteLine(mensagem);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

