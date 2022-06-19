Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o => new Remover(o).Run());