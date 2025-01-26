using PirvarslerLib;

try
{
  await Pirvarsler.CheckAndNotify();
}

catch (Exception ex)
{
  Console.WriteLine(ex);
}
