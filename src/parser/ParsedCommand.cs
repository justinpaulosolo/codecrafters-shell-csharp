using CodeCrafters.Shell.Commands;

internal record ParsedCommand(Command? Command, string? StdoutTarget, string? StderrTarget, bool StdoutAppend, bool StderrAppend);