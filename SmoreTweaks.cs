using GDWeave;
using GDWeave.Modding;
using GDWeave.Godot; 
using GDWeave.Godot.Variants; 
using System.Collections.Generic;

namespace SmoreTweaks 
{
    public class SmoreTweaks : IScriptMod {
        private IModInterface _modInterface; 

        public SmoreTweaks(IModInterface modInterface) {
            _modInterface = modInterface; 
            _modInterface.Logger.Information("SmoreTweaks mod is locked and loaded.");
        }

        public bool ShouldRun(string path)
        {
            // Check if the script path matches the target script where you want to modify the brush_size
            var shouldRun = path == "res://Scenes/Minigames/ScratchTicket/scratch_ticket.gdc";
            // **LOGSPAM ALERT***
            // _modInterface.Logger.Information("ShouldRun called with path: {Path}. Result: {Result}", path, shouldRun);
            return shouldRun;
        }

        public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
            var enumerator = tokens.GetEnumerator();

            while (enumerator.MoveNext()) {
                var token = enumerator.Current;

                // Check if the token is the 'brush_size' identifier
                if (token is IdentifierToken identifierToken && identifierToken.Name == "brush_size") {
                    // Peek at the next token to check if it's an assignment operator
                    if (enumerator.MoveNext()) {
                        var nextToken = enumerator.Current;

                        if (nextToken.Type == TokenType.OpAssign) {
                            _modInterface.Logger.Information("Found brush_size assignment. Modifying the value...");

                            // Yield the 'brush_size' token
                            yield return token;

                            // Yield the assignment operator '='
                            yield return nextToken;

                            // Replace the value with 20
                            yield return new ConstantToken(new IntVariant(20));

                            // Optionally, yield a newline if needed
                            // yield return new Token(TokenType.Newline, 0);

                            // Skip the original value token
                            if (enumerator.MoveNext()) {
                                // We have already replaced the value, so we skip this token
                                _modInterface.Logger.Information("Skipping original value token.");
                            }

                            continue;
                        }
                        else {
                            // Not an assignment, yield both tokens
                            yield return token;
                            yield return nextToken;
                            continue;
                        }
                    }
                    else {
                        // No more tokens, yield the current token
                        yield return token;
                        break;
                    }
                }

                // Not 'brush_size', yield the token
                yield return token;
            }

            _modInterface.Logger.Information("Finished processing tokens for path: {Path}", path);
        }
    }
}