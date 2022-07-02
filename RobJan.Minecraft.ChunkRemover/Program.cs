Parser.Default.ParseArguments<Options, UseConfigOptions>(args)
    .WithParsed<Options>(o => new Remover(o).Run())
    .WithParsed<UseConfigOptions>(o => new Remover(o).Run());
