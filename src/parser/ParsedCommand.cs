using CodeCrafters.Shell.Commands;

namespace CodeCrafters.Shell.parser;

internal record ParsedCommand(Command? Command, string? StdoutTarget, string? StderrTarget, bool StdoutAppend, bool StderrAppend);